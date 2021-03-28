using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Prototype.Data.Player
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