using RH.Utilities.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.Logs
{
    public static class LogsHandler
    {
        public static event Action<int, int, int> OnLogsCountUpdated;
        public static event Action<string, string, LogType> OnNewLogRecieved;

        private static int _logsCount;
        private static int _warningsCount;
        private static int _errorsCount;

        public static void Init() =>
            Application.logMessageReceived += Application_logMessageReceived;

        public static void Dispose() =>
            Application.logMessageReceived -= Application_logMessageReceived;

        public static void Clear()
        {
            _logsCount = _warningsCount = _errorsCount = 0;
            OnLogsCountUpdated?.Invoke(_logsCount, _warningsCount, _errorsCount);
        }

        private static void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            switch (type)
            {
                case LogType.Exception:
                case LogType.Error:
                    _errorsCount++;
                    break;
                case LogType.Warning:
                    _warningsCount++;
                    break;
                case LogType.Log:
                case LogType.Assert:
                    _logsCount++;
                    break;
                default:
                    break;
            }

            OnLogsCountUpdated?.Invoke(_logsCount, _warningsCount, _errorsCount);
            OnNewLogRecieved?.Invoke(condition, stackTrace, type);
        }
    }
}