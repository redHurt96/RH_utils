using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RH.Utilities.SavingSystem.Local
{
    public class LocalBinSaveSystem : LocalSaveSystem
    {
        public LocalBinSaveSystem(string path, string fileName) : base(path, fileName) { }

        public override void Save<T>(T saveObject)
        {
            using (var dataStream = new FileStream(FullName, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(dataStream, saveObject);
            }
        }

        protected override void LoadFile<T>(Action<T> onDone)
        {
            using (var dataStream = new FileStream(FullName, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var savedObject = formatter.Deserialize(dataStream) as T;
                onDone?.Invoke(savedObject);
            }
        }
    }
}