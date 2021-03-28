using RH.Utilities.SceneManagement.Core;
using UnityEditor;

namespace RH.Utilities.SceneManagement.Test
{
    /// <summary>
    /// Методы для переходов создаются по аналогии с тестовыми - "ShowFirstScene" и "ShowSecondScene".
    /// </summary>
    public static class GameSceneChanger
    {
        public static void ShowFirstScene() => SceneChanger.ChangeScene(1); 
        public static void ShowSecondScene() => SceneChanger.ChangeScene(2);
    }
}