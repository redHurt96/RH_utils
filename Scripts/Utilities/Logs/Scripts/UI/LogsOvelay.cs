using RH.Utilities.Attributes;
using RH.Utilities.Logs;
using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.Logs
{
    public class LogsOvelay : MonoSingleton<LogsOvelay>
    {
        [SerializeField] private LogsMiniPanel _logsMiniPanel;
        [SerializeField] private LogsBigPanel _logsBigPanel;

        [ReadOnly, SerializeField] private bool _isInit;
        [ReadOnly, SerializeField] private bool _isDisposed;

        public void ShowLogsBigPanel()
        {
            _logsBigPanel.gameObject.SetActive(true);
        }

        private void Start() => Init();

        private void Init()
        {
            if (_isInit)
                return;
            _isInit = true;

            LogsHandler.Init();

            _logsBigPanel.Init();
            _logsMiniPanel.Init();
        }

        private void OnDestroy()
        {
            LogsHandler.Dispose();

            _logsBigPanel.Dispose();
            _logsMiniPanel.Dispose();
        }
    }
}