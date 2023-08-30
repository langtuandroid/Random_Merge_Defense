using System.Collections;
using System.Collections.Generic;
using SaveData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
    [SerializeField] Button buildButton;
    TextMeshProUGUI text;
    private InGameData inGameData;
    int requiredGold;
    bool on = true;
    public void Initialize()
    {
        inGameData = DataManager.Database.InGameDataLayer.GetData();
        buildButton.onClick.AddListener(TowerManager.Instance.RandomBuildTower);
        text = buildButton.GetComponentInChildren<TextMeshProUGUI>();
        Update();
    }
    private void Update()
    {
        if (inGameData.goldAmount >= requiredGold)
        {
            On();
        }
        else
        {
            Off();
        }
    }
    public void UpdateText(int requiredGold)
    {
        this.requiredGold = requiredGold;
        text.text = string.Format($"생성 골드량\n{this.requiredGold}");
    }
    void On()
    {
        if (!on)
        {
            on = true;
            buildButton.interactable = on;
        }
    }
    void Off()
    {
        if (on)
        {
            on = false;
            buildButton.interactable = on;
        }
    }
}
