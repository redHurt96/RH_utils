using System;
using UnityEngine;

namespace RH.Utilities.SceneManagement.Core
{
    public interface ISceneReciever
    {
        void ShowScene(Action onDone);
        void HideScene(Action onDone);
    }
}