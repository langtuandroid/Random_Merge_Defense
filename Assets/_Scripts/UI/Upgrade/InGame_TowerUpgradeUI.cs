using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

public class InGame_TowerUpgradeUI : MonoBehaviour
{
    [SerializeField] InGame_TowerUpgradeButton upgradeButtonPrefab;
    [SerializeField] List<InGame_TowerUpgradeButton> upgradeButtons = new List<InGame_TowerUpgradeButton>();
    [SerializeField] Transform frame;
    InGameData inGameData;
    int buttonCount;
    public void Initialize()
    {
        inGameData = DataManager.Database.InGameDataLayer.GetData();
        var inGameTowerUpgrades = inGameData.inGameTowerUpgrades;

        for (int i = 0; i < inGameTowerUpgrades.Count; i++)
        {
            upgradeButtons.Add(Instantiate(upgradeButtonPrefab, frame));
            upgradeButtons[i].Initialize(inGameTowerUpgrades[i]);
        }
        Destroy(upgradeButtonPrefab.gameObject);
        buttonCount = upgradeButtons.Count;
    }
    private void Update()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            if (inGameData.goldAmount >= upgradeButtons[i].NextUpgradeInfo.goldAmount)
            {
                upgradeButtons[i].On();
            }
            else
            {
                upgradeButtons[i].Off();
            }
        }
    }
}
