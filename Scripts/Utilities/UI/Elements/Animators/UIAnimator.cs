using RH.Utilities.UI.Elements.Core;
using System;
using UnityEngine;

namespace RH.Utilities.UI.Elements
{
    public class UIAnimator : MonoBehaviour, IUiAnimator 
    {
        private Animator _animator;
        private Action _onDone;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Show(Action onDone)
        {
            _onDone = onDone;
            gameObject.SetActive(true);
            _animator.Play("Show");
        }

        public void Hide(Action onDone)
        {
            _onDone = onDone;
            _animator.Play("Hide");
        }

        private void CompleteShow()
        {
            InvokeOnDone();
        }
        
        private void CompleteHide()
        {
            InvokeOnDone();
            gameObject.SetActive(false);
        }

        private void InvokeOnDone()
        {
            _onDone?.Invoke();
            _onDone = null;
        }
    }
}