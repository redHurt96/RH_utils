using System;

namespace RH.Utilities.SavingSystem
{
    public interface ISaveSystem
    {
        bool CanLoad { get; }

        void Save<T>(T saveObject) where T : class;
        void Load<T>(Action<T> onDone) where T : class;
    }
}