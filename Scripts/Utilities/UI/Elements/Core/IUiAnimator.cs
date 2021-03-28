using System;

namespace RH.Utilities.UI.Elements.Core
{
    public interface IUiAnimator
    {
        void Show(Action callback);
        void Hide(Action callback);
    }
}