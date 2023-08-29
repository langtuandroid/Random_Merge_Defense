using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonComponent<StageManager>
{
    WaveManager waveManager;
    public void Awake()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        var stageData = DataManager.Database.StageDataLayer.GetData();
        if (stageData == null)
        {
            stageData = new SaveData.StageData
            {
                currentStageId = 1,
                currentWaveOrder = 0
            };
            DataManager.Database.StageDataLayer.SetData(stageData);
        }

        var playerData = DataManager.Database.PlayerDataLayer.GetData();
        if (playerData == null)
        {
            playerData = new SaveData.PlayerData();
            var towerStatusDataTables = DataManager.DataTableBase.TowerStatusDataTable.GetTables();
            for (int i = 0; i < towerStatusDataTables.Length; i++)
            {
                playerData.ownDeckTowerIds.Add(towerStatusDataTables[i].towerId);
            }
            DataManager.Database.PlayerDataLayer.SetData(playerData);
        }


        var inGameData = DataManager.Database.InGameDataLayer.GetData();
        if (inGameData == null)
        {
            inGameData = new SaveData.InGameData();

            DataManager.Database.InGameDataLayer.SetData(inGameData);
        }
        SeatTileList.Instance.Initialize();

        waveManager.Initialize(stageData.currentStageId, stageData.currentWaveOrder);
        InGameUI.Instance.Initialize();
        InGame_TowerUpgradeManager.Instance.Initialize();
        TowerManager.Instance.Initialize();
    }
    public void Fail()
    {

    }
    public void Clear()
    {

    }
}
