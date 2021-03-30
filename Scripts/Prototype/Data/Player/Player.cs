namespace RH.Prototype.Data
{
    public class Player
    {
        public static Player Instance { get; private set; }

        public PlayerData Data;

        private Player(PlayerData data)
        {
            if (data == null)
                data = new PlayerData();

            Data = data;
        }
    
        public static Player CreateInstance(PlayerData data = null)
        {
            if (Instance == null)
                Instance = new Player(data);

            return Instance;
        }
    }
}