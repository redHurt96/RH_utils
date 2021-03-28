using RH.Prototype.Data;
using RH.Prototype.Data.Library;
using RH.Prototype.Data.Player;
using RH.Utilities.SavingSystem;
using UnityEngine;

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
            throw new System.Exception($"Can't find game library");
        else
            _libraryLoader.Load<GameLibraryData>(GetLibrary);

        void GetLibrary(GameLibraryData data) => GameLibrary.CreateInstance(data);
    }
}