using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RH.Utilities.Logs
{
    public class LogsBigPanel : MonoSingleton<LogsBigPanel>
    {
        [Header("Logs counts")]
        [SerializeField] private Text _logsCount;
        [SerializeField] private Text _warningsCount;
        [SerializeField] private Text _errorsCount;

        [Header("Logs")]
        [SerializeField] private LogPrefab _logPrefab;
        [SerializeField] private Transform _logsParent;
        [SerializeField] private int _maxLogsCount = 2000;

        [Header("Buttons")]
        [SerializeField] private Button _logSortButton;
        [SerializeField] private Button _warningSortButton;
        [SerializeField] private Button _errorSortButton;
        [SerializeField] private Button _copyButton;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeCopyFieldButton;

        [Header("Copy")]
        [SerializeField] private InputField _copyField;
        [SerializeField] private GameObject _copyFieldParent;
        [SerializeField] private bool _copyWithStackTrace = false;

        [Header("Select log")]
        [SerializeField] private Text _currentLog;

        private List<LogPrefab> _addedLogs = new List<LogPrefab>();
        private LogSorting _currentSorting;

        private enum LogSorting
        {
            None, Assert, Log, Warning, Error
        }

        public void Init()
        {
            LogsHandler.OnLogsCountUpdated += OnLogsCountUpdate;
            LogsHandler.OnNewLogRecieved += OnGetNewLog;

            _logSortButton.onClick.AddListener(() => OnSortButtonClick(LogType.Log));
            _warningSortButton.onClick.AddListener(() => OnSortButtonClick(LogType.Warning));
            _errorSortButton.onClick.AddListener(() => OnSortButtonClick(LogType.Error));

            _copyButton.onClick.AddListener(PrepareLogsToCopy);
            _clearButton.onClick.AddListener(ClearLogs);
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));

            _closeCopyFieldButton.onClick.AddListener(() => _copyFieldParent.SetActive(false));
        }

        public void Dispose()
        {
            LogsHandler.OnLogsCountUpdated -= OnLogsCountUpdate;
            LogsHandler.OnNewLogRecieved -= OnGetNewLog;

            _logSortButton.onClick.RemoveAllListeners();
            _warningSortButton.onClick.RemoveAllListeners();
            _errorSortButton.onClick.RemoveAllListeners();

            _copyButton.onClick.RemoveListener(PrepareLogsToCopy);
            _clearButton.onClick.RemoveListener(ClearLogs);
            _closeButton.onClick.RemoveAllListeners();

            _closeCopyFieldButton.onClick.RemoveAllListeners();
        }

        private void OnGetNewLog(string message, string trace, LogType type)
        {
            if (_addedLogs.Count > _maxLogsCount)
                _addedLogs.RemoveAt(0);

            _addedLogs.Add(Instantiate(_logPrefab, _logsParent).SetLog(new Log(message, trace, type)));
        }

        private void OnLogsCountUpdate(int logs, int warnings, int errors)
        {
            _logsCount.text = logs.ToString();
            _warningsCount.text = warnings.ToString();
            _errorsCount.text = errors.ToString();
        }

        private void PrepareLogsToCopy()
        {
            if (_addedLogs.Count == 0) return;

            if (_copyFieldParent.activeSelf)
                _copyFieldParent.SetActive(false);
            else
            {
                var allLogs = new StringBuilder();

                foreach (var logPrefab in _addedLogs)
                {
                    allLogs.Append($"{logPrefab.Log.Type} : {logPrefab.Log.Message}");
                    if (_copyWithStackTrace)
                        allLogs.Append($"\n{logPrefab.Log.Trace}");
                    allLogs.Append("\n");
                }

                _copyField.text = allLogs.ToString();
                _copyFieldParent.SetActive(true);
            }
        }

        private void OnSortButtonClick(LogType type)
        {
            if (CompareLogTypeAndSorting(type, _currentSorting))
                ShowAllLogs();
            else SortLogs(type);

            bool CompareLogTypeAndSorting(LogType logType, LogSorting sorting) => logType.ToString().Equals(sorting.ToString());

            void SortLogs(LogType logType)
            {
                foreach (var prefab in _addedLogs)
                    prefab.gameObject.SetActive(prefab.Log.Type == logType || (logType == LogType.Error && prefab.Log.Type == LogType.Exception));

                _currentSorting = SetLogSortingType(logType);
            }

            void ShowAllLogs()
            {
                foreach (var prefab in _addedLogs)
                    prefab.gameObject.SetActive(true);

                _currentSorting = LogSorting.None;
            }

            LogSorting SetLogSortingType(LogType logType)
            {
                switch (logType)
                {
                    case LogType.Error:
                    case LogType.Exception:
                        return LogSorting.Error;
                    case LogType.Assert:
                        return LogSorting.Assert;
                    case LogType.Warning:
                        return LogSorting.Warning;
                    case LogType.Log:
                        return LogSorting.Log;
                    default:
                        return LogSorting.None;
                }
            }
        }

        public void ShowLog(Log log)
        {
            _currentLog.color = log.Color;
            _currentLog.text = log.ToString();
        }

        private void ClearLogs()
        {
            foreach (var prefab in _addedLogs)
                Destroy(prefab.gameObject);

            _addedLogs.Clear();

            LogsHandler.Clear();
        }
    }
}