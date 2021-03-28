using System;
using UnityEngine;

namespace RH.Utilities.UI.Elements.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIElement : MonoBehaviour
    {
        public bool IsShowed => state == UIElementState.Showed;
        public new string name => gameObject.name;

        protected CanvasGroup _canvasGroup;

        [SerializeField] private UIElement[] _childs;

        private UIElementState state = UIElementState.Hided;
        private IUiAnimator _animator;

        public void Open(Action callback = null)
        {
            if (_animator == null)
                _animator = InitAnimator();

            SetState(UIElementState.InShowTransition);

            AttachListeners();
            PerformBeforeOpen();
            AddOnOpenedBehavior(callback);
            StartShowing(callback);
        }

        public void Close(Action callback = null)
        {
            SetState(UIElementState.InHideTransition);

            DetachListeners();
            PerformBeforeClose();
            StartHiding(callback);
        }

        protected abstract IUiAnimator InitAnimator();
        protected virtual void AttachListeners() { }
        protected virtual void DetachListeners() { }
        protected virtual void PerformBeforeOpen() { }
        protected virtual void PerformBeforeClose() { }
        protected virtual void PerformOnOpen() { }

        #region PRIVATE METHODS

        private void SetState(UIElementState newState)
        {
            if (state != newState)
                state = newState;
            else
                throw new Exception($"[UIElement] Can't go to state {newState}, because it's a current state");
        }

        private void StartShowing(Action callback = null)
        {
            foreach (var element in _childs)
                element.Open();

            _animator.Show(() =>
            {
                SetState(UIElementState.Showed);
                callback?.Invoke();
            });
        }

        private void StartHiding(Action onDone = null)
        {
            foreach (var element in _childs)
                element.Close();

            _animator.Hide(() =>
            {
                SetState(UIElementState.Hided);
                onDone?.Invoke();
            });
        }

        private void AddOnOpenedBehavior(Action onDone)
        {
            onDone = () =>
            {
                PerformOnOpen();
                onDone?.Invoke();
            };
        }

        #endregion
        
        public enum UIElementState
        {
            Hided = 0,
            InShowTransition,
            Showed,
            InHideTransition
        }
    }
}