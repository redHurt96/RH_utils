using RH.Utilities.UI.Management;
using System.IO;
using UnityEngine;

public class TestWindowsManager : MonoBehaviour
{
    private UIManager _manager;
    private string _folderPath = "SceneManagementTest";

    private void Start()
    {
        _manager = UIManager.CreateInstance(AppLinks.Instance.WindowsParent);
    }

    [ContextMenu("Window 0")]
    private void OpenTestWindow0() => _manager.Open(Path.Combine(_folderPath, "TestWindow0"));
    [ContextMenu("Window 1")]
    private void OpenTestWindow1() => _manager.Open(Path.Combine(_folderPath, "TestWindow1"));
}
