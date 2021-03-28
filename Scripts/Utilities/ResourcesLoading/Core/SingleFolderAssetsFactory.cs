using System;
using System.IO;
using UnityEngine;

namespace RH.Utilities.ResourcesManagement
{
    public class SingleFolderAssetsFactory : IAssetsFactory
    {
        public event Action<GameObject> OnAssetCreated;

        private string _folderName;

        public SingleFolderAssetsFactory(string name) => _folderName = name;

        public virtual GameObject CreateObject(string assetName, Transform parent = null)
        {
            var loadableObject = Resources.Load(FullName(assetName));
            var instantiatedObject = MonoBehaviour.Instantiate(loadableObject, parent) as GameObject;

            RenameGameObject(instantiatedObject);
            
            OnAssetCreated?.Invoke(instantiatedObject);

            return instantiatedObject;
        }

        public virtual T CreateObject<T>(string resourceName, Transform parent) where T : Component =>
            CreateObject(resourceName, parent)?.GetComponent<T>();

        private string FullName(string assetName) => Path.Combine(_folderName, assetName);

        private void RenameGameObject(GameObject gameObject)
        {
            var newName = gameObject.name.Replace("(Clone)", "");
            gameObject.name = newName;
        }
    }

}