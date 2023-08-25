using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonComponent<StageManager>
{
    WaveManager waveManager;
    public void Initialize()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        var stageData = DataManager.Database.StageDataLayer.GetData();
        waveManager.Initialize(stageData.currentStageId, stageData.currentWaveIndex);
    }
    public void Fail()
    {

    }
    public void Clear()
    {

    }
}
