using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class InGame_TowerUpgradeButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI text;
    InGameTowerUpgrade inGameTowerUpgrade;
    NextUpgradeInfo nextUpgradeInfo;
    public NextUpgradeInfo NextUpgradeInfo => nextUpgradeInfo;
    bool on;
    public void Initialize(InGameTowerUpgrade inGameTowerUpgrade)
    {
        this.inGameTowerUpgrade = inGameTowerUpgrade;
        button.onClick.AddListener(Upgrade);
        SetNextUpgradeInfo();
    }
    void Upgrade()
    {
        GoldManager.Instance.DecreaseGold(nextUpgradeInfo.goldAmount);
        InGame_TowerUpgradeManager.Instance.Upgrade(inGameTowerUpgrade.towerId);
        SetNextUpgradeInfo();

    }
    void SetNextUpgradeInfo()
    {
        int goldAmount = InGame_TowerUpgradeManager.Instance.RequiredInitialGold + (inGameTowerUpgrade.upgradeLevel * inGameTowerUpgrade.goldIncrease);
        nextUpgradeInfo = new NextUpgradeInfo(goldAmount, inGameTowerUpgrade.upgradeLevel);
        text.text = string.Format($"{inGameTowerUpgrade.towerId} : {nextUpgradeInfo.level}\n gold = {nextUpgradeInfo.goldAmount}");
    }
    public void On()
    {
        if (on) return;
        on = true;
        button.interactable = true;
    }
    public void Off()
    {
        if (!on) return;
        on = false;
        button.interactable = false;
    }
}
