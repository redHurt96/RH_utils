using RH.Utilities.UI.Elements.Core;

namespace RH.Utilities.UI.Elements
{
    public class AnimatedWindow : BaseWindow
    {
        protected override IUiAnimator InitAnimator() => GetComponent<UIAnimator>();
    }
}