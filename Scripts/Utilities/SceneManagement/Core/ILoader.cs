using System;

namespace RH.Utilities.SceneManagement.Core
{
    public interface ILoader
    {
        void Show(Action onDone);
        void Hide(Action onDone);
    }
}