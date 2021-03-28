using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace RH.Utilities.Logs
{
    public class LogPrefab : MonoBehaviour
    {
        [SerializeField] private Text _logMessage;
        [SerializeField] private Button _showFullLogButton;

        public Log Log { get; private set; }

        public LogPrefab SetLog(Log log)
        {
            Log = log;

            _logMessage.text = log.Message;
            _logMessage.color = log.Color;

            _showFullLogButton.onClick.AddListener(() => LogsBigPanel.Instance.ShowLog(Log));

            return this;
        }

        private void OnDestroy()
        {
            _showFullLogButton.onClick.RemoveAllListeners();
        }
    }

    public struct Log
    {
        public string Message;
        public string Trace;
        public LogType Type;

        public Color Color => GetColor();

        public Log(string message, string trace, LogType type)
        {
            Message = message;
            Trace = trace;
            Type = type;
        }

        private Color GetColor()
        {
            switch (Type)
            {
                case LogType.Assert:
                    return Color.grey;
                case LogType.Error:
                case LogType.Exception:
                    return Color.red;
                case LogType.Warning:
                    return Color.yellow;
                case LogType.Log:
                    return Color.white;
                default:
                    return Color.grey;
            }
        }

        public override string ToString()
        {
            return $"{Type} : {Message}\n{Trace}";
        }
    }
}