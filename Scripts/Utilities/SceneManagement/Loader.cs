using RH.Utilities.SceneManagement.Core;
using System;
using UnityEngine;

namespace RH.Utilities.SceneManagement
{
    public class Loader : MonoBehaviour, ILoader
    {
        [SerializeField] private Animator _animator;

        private Action _onShow;
        private Action _onHide;

        public void Init()
        {
            SceneChanger.AddLoader(this);
            gameObject.SetActive(false);
        }

        public void Show(Action onDone)
        {
            gameObject.SetActive(true);
            _onShow = onDone;
            _animator.Play("Show");
        }

        public void Hide(Action onDone)
        {
            _onHide = onDone;
            _animator.Play("Hide");
        }

        #region CALL FROM ANIMATOR
        public void InvokeOnShowEvent()
        {
            _onShow.Invoke();
            _onShow = null;
        }
        public void InvokeOnHideEvent()
        {
            gameObject.SetActive(false);
            _onHide.Invoke();
            _onHide = null;
        }
        #endregion
    }
}