using RH.Utilities.SavingSystem;

namespace RH.Prototype.Data
{
    public class DataManager
    {
        public static DataManager Instance { get; private set; }

        private ISaveSystem _playerDataSaveSystem;
        private ISaveSystem _libraryLoader;

        private DataManager()
        {
            Init();
        }

        public static DataManager CreateInstance()
        {
            if (Instance == null)
                Instance = new DataManager();

            return Instance;
        }

        public void Init()
        {
            InitLibrary();
            InitPlayer();
        }

        private void InitPlayer()
        {
            _playerDataSaveSystem = SaveSystemCreator.CreatePlayerDataSaveSystem();

            if (_playerDataSaveSystem.CanLoad)
                _playerDataSaveSystem.Load<PlayerData>(LoadPlayer);
            else
                CreateNewPlayer();

            void LoadPlayer(PlayerData data) => Player.CreateInstance(data);
            void CreateNewPlayer() => Player.CreateInstance();
        }

        private void InitLibrary()
        {
            _libraryLoader = SaveSystemCreator.CreateLibraryLoader();

            if (_libraryLoader.CanLoad)
                _libraryLoader.Load<GameLibraryData>(GetLibrary);
            else
                throw new System.Exception($"Can't find game library");

            void GetLibrary(GameLibraryData data) => GameLibrary.CreateInstance(data);
        }
    }
}