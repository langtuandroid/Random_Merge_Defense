using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InGame_TowerUpgradeManager : SingletonComponent<InGame_TowerUpgradeManager>
{
    List<InGameTowerUpgrade> inGameTowerUpgrades = new List<InGameTowerUpgrade>();
    public void Initialize()
    {
        var playerData = DataManager.Database.PlayerDataLayer.GetData();
        for (int i = 0; i < playerData.ownDeckTowerIds.Count; i++)
        {
            var inGame_TowerUpgradeDataTable = DataManager.DataTableBase.InGame_TowerUpgradeDataTable.GetTowerAbilityDataTable(playerData.ownDeckTowerIds[i]);
            inGameTowerUpgrades.Add(new InGameTowerUpgrade(playerData.ownDeckTowerIds[i], 0, inGame_TowerUpgradeDataTable.upgradeValue));
        }
    }
    public void Upgrade(string towerId)
    {
        for (int i = 0; i < inGameTowerUpgrades.Count; i++)
        {
            if (inGameTowerUpgrades[i].towerId == towerId)
            {
                inGameTowerUpgrades[i].Upgrade();
                return;
            }
        }
    }
    public InGameTowerUpgrade GetInGameTowerUpgrade(string towerId)
    {
        for (int i = 0; i < inGameTowerUpgrades.Count; i++)
        {
            if (inGameTowerUpgrades[i].towerId == towerId)
            {
                return inGameTowerUpgrades[i];
            }
        }
        return null;
    }
}
