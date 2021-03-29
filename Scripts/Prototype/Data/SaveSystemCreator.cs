using RH.Utilities.SavingSystem;
using RH.Utilities.SavingSystem.Local;
using UnityEngine;

namespace RH.Prototype.Data
{
    public class SaveSystemCreator
    {
        public const string PLAYER_DATA_FILENAME = "PlayerData";
        public const string GAME_LIBRARY_FILENAME = "GameLibrary";

        public static ISaveSystem CreatePlayerDataSaveSystem() => CreateLocalSaveSystem(Application.persistentDataPath, PLAYER_DATA_FILENAME);
        public static ISaveSystem CreateLibraryLoader() => CreateLocalSaveSystem(Application.streamingAssetsPath, GAME_LIBRARY_FILENAME);

        private static ISaveSystem CreateLocalSaveSystem(string path, string fileName)
        {
            ISaveSystem system;

#if UNITY_EDITOR
            system = new LocalJsonSaveSystem(path, fileName);
#else
            system = new LocalBinSaveSystem(path, fileName);
#endif

            return system;
        }
    }
}