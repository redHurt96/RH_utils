using RH.Utilities.Singleton;

namespace RH.Prototype.Data.Library
{
    public class GameLibrary
    {
        public static GameLibrary Instance { get; private set; }

        public GameLibraryData Data { get; private set; }

        private GameLibrary(GameLibraryData data)
        {
            Data = data;
        }

        public static GameLibrary CreateInstance(GameLibraryData data)
        {
            if (Instance == null)
                Instance = new GameLibrary(data);

            return Instance;
        }
    }
}