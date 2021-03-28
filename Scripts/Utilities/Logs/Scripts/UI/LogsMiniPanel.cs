using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace RH.Utilities.Logs
{
    public class LogsMiniPanel : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Text _logsCountText;
        [SerializeField] private Button _button;

        private RectTransform _rectTransform;

        public void Init()
        {
            LogsHandler.OnLogsCountUpdated += UpdateLogsCount;
            _rectTransform = GetComponent<RectTransform>();
            _button?.onClick.AddListener(() => LogsOvelay.Instance.ShowLogsBigPanel());
        }

        public void Dispose()
        {
            LogsHandler.OnLogsCountUpdated -= UpdateLogsCount;
        }

        private void UpdateLogsCount(int logsCount, int warningsCount, int errorsCount) =>
            _logsCountText.text = $"<color=green>{logsCount}</color>/<color=yellow>{warningsCount}</color>/<color=red>{errorsCount}</color>";

        public void OnDrag(PointerEventData eventData) =>
            _rectTransform.anchoredPosition += eventData.delta;
    }
}