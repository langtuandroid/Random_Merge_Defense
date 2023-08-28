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
        waveManager.Initialize(stageData.currentStageId, stageData.currentWaveOrder);
        InGameUI.Instance.Initialize();
    }
    public void Fail()
    {

    }
    public void Clear()
    {

    }
}
