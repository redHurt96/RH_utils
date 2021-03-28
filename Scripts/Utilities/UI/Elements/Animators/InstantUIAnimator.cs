using RH.Utilities.UI.Elements.Core;
using System;

namespace RH.Utilities.UI.Elements
{
    public class InstantUIAnimator : IUiAnimator
    {
        private UIElement _element;

        public InstantUIAnimator(UIElement element)
        {
            _element = element;
        }

        public void Show(Action callback)
        {
            _element.gameObject.SetActive(true);
            callback?.Invoke();
        }
        
        public void Hide(Action callback)
        {
            _element.gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
}