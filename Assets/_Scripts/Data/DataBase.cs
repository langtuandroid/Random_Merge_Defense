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
        [SerializeField] public StageDataLayer StageDataLayer;
        public void Initialize()
        {
            StageDataLayer = new StageDataLayer();
            PlayerDataLayer = new PlayerDataLayer();
        }
    }
}