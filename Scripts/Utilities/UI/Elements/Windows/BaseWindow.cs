using RH.Utilities.UI.Elements.Core;
using RH.Utilities.UI.Management;
using UnityEngine;
using UnityEngine.UI;

namespace RH.Utilities.UI.Elements
{
    public class BaseWindow : UIElement
    {
        [SerializeField] protected Button _closeButton;

        //NOTE: when overridden, don't use the base method
        protected override IUiAnimator InitAnimator() => new InstantUIAnimator(this);

        //NOTE: when overridden, use the base method for the default _closeButton behavior
        protected override void AttachListeners()
        {
            _closeButton?.onClick.AddListener(CloseWindow);
        }

        //NOTE: when overridden, use the base method for the default _closeButton behavior
        protected override void DetachListeners()
        {
            _closeButton?.onClick.RemoveListener(CloseWindow);
        }

        protected void CloseWindow() => UIManager.Instance.Close(this);
    }
}