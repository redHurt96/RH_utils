using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.Hacks
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DebugPanel : MonoSingleton<DebugPanel>
    {
        private CanvasGroup _canvasGroup;

        private bool _isInitialized = false;

        private void Awake() => Init();

        private void Init()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.blocksRaycasts = false;

            InitContent();
        }

        [ContextMenu("Open")]
        public void Open()
        {
            Init();

            OpenContent();

            gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;
        }

        [ContextMenu("Close")]
        public void Close()
        {
            CloseContent();
            _canvasGroup.alpha = 0f;
        }

        protected virtual void InitContent() { }
        protected virtual void OpenContent() { }
        protected virtual void CloseContent() { }
    }
}