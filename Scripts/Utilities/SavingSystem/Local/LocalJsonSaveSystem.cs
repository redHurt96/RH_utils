using System;
using System.IO;
using UnityEngine;

namespace RH.Utilities.SavingSystem.Local
{
    public class LocalJsonSaveSystem : LocalSaveSystem
    {
        protected override string FullName => base.FullName + ".json";

        public LocalJsonSaveSystem(string path, string fileName) : base(path, fileName) { }

        public override void Save<T>(T saveObject)
        {
            var jsonData = JsonUtility.ToJson(saveObject);
            File.WriteAllText(FullName, jsonData);
        }

        protected override void LoadFile<T>(Action<T> onDone)
        {
            var jsonData = File.ReadAllText(FullName);
            onDone?.Invoke(JsonUtility.FromJson<T>(jsonData));
        }
    }
}