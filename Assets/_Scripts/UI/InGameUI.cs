using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : SingletonComponent<InGameUI>
{
    public WaveUI WaveUI;
    public TopUI TopUI;
    public BuildUI BuildUI;
    public InGame_TowerUpgradeUI InGame_TowerUpgradeUI;
    public GoldUI GoldUI;
    public void Initialize()
    {
        WaveUI.Initialize();
        TopUI.Initialize();
        BuildUI.Initialize();
        InGame_TowerUpgradeUI.Initialize();
    }
}
