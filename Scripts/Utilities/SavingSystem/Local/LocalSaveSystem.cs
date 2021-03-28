using System;
using System.IO;
using UnityEngine;

namespace RH.Utilities.SavingSystem
{
    public abstract class LocalSaveSystem : ISaveSystem
    {
        public bool CanLoad => File.Exists(FullName);

        protected virtual string FullName => Path.Combine(path, fileName);

        protected readonly string path;
        protected readonly string fileName;

        public LocalSaveSystem(string path, string fileName)
        {
            this.path = path;
            this.fileName = fileName;
        }

        public void Load<T>(Action<T> onDone) where T : class
        {
            if (CanLoad)
                LoadFile<T>(onDone);
            else
                Debug.LogError($"[{GetType().Name}]: file {FullName} doesn't exist");
        }

        public abstract void Save<T>(T saveObject) where T : class;
        
        protected abstract void LoadFile<T>(Action<T> onDone) where T : class;
    }
}