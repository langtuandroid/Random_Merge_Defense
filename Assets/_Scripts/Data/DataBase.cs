using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
namespace SaveData
{
    [System.Serializable]
    public class DataBase
    {

        public void Save() => SaveGame.Save("Data", this);
        [SerializeField] public PlayerDataLayer PlayerDataLayer;
        public void Initialize()
        {

            PlayerDataLayer = new PlayerDataLayer();
        }
    }
}