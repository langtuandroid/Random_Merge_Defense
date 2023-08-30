using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InGame_TowerUpgradeManager : SingletonComponent<InGame_TowerUpgradeManager>
{
    List<InGameTowerUpgrade> inGameTowerUpgrades = new List<InGameTowerUpgrade>();
    public void Initialize()
    {
        inGameTowerUpgrades = DataManager.Database.InGameDataLayer.GetData().inGameTowerUpgrades;
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
