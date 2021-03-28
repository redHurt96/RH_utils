using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RH.Utilities.ResourcesManagement
{
    public class DefaultAssetsFactory : IAssetsFactory
    {
        public event Action<GameObject> OnAssetCreated;

        public GameObject CreateObject(string assetName, Transform parent = null)
        {
            var loadableObject = Resources.Load(assetName);
            var instantiatedObject = MonoBehaviour.Instantiate(loadableObject, parent) as GameObject;

            RenameGameObject(instantiatedObject);
            OnAssetCreated?.Invoke(instantiatedObject);

            return instantiatedObject;
        }

        public T CreateObject<T>(string resourceName, Transform parent) where T : Component =>
            CreateObject(resourceName, parent)?.GetComponent<T>();

        private void RenameGameObject(GameObject gameObject)
        {
            var newName = gameObject.name.Replace("(Clone)", "");
            gameObject.name = newName;
        }
    }
}