using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

public class GoldManager : SingletonComponent<GoldManager>
{
    InGameData inGameData;
    public void Initialize()
    {
        inGameData = DataManager.Database.InGameDataLayer.GetData();
        InGameUI.Instance.GoldUI.SetGoldText(inGameData.goldAmount);
    }

    public void IncreaseGold(int value)
    {
        inGameData.goldAmount += value;
        InGameUI.Instance.GoldUI.SetGoldText(inGameData.goldAmount);
    }
    public void DecreaseGold(int value)
    {
        inGameData.goldAmount -= value;
        InGameUI.Instance.GoldUI.SetGoldText(inGameData.goldAmount);
    }
    public void SetGold(int value)
    {
        inGameData.goldAmount = value;
        InGameUI.Instance.GoldUI.SetGoldText(inGameData.goldAmount);
    }
}
