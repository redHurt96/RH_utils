using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.ResourcesManagement
{
    public interface IAssetsFactory
    {
        GameObject CreateObject(string assetName, Transform parent);
        T CreateObject<T>(string assetName, Transform parent) where T : Component;
    }
}