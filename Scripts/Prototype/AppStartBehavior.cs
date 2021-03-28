using RH.Utilities.SceneManagement;
using RH.Utilities.SceneManagement.Core;
using RH.Utilities.SceneManagement.Test;
using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppStartBehavior : MonoSingleton<AppStartBehavior>
{
    private async void Start()
    {
        AppLinks.Instance.Loader.Init();
        DataManager.CreateInstance();

        await Task.Delay(200);

        GameSceneChanger.ShowFirstScene();
    }
}
