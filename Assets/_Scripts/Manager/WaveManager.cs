using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : SingletonComponent<WaveManager>
{
    EnemySpawnManager enemySpawnManager;
    int currentWaveOrder;
    public void Initialize(int stageId, int currentWaveOrder)
    {
        this.currentWaveOrder = currentWaveOrder;
        enemySpawnManager = GetComponentInChildren<EnemySpawnManager>();
        enemySpawnManager.Initialize(stageId, currentWaveOrder);
    }
    public void WaveEnd()
    {

    }
    public void WaveStartImmediately()
    {
        InGameUI.Instance.WaveUI.Off();
        enemySpawnManager.WaveStartImmediately();
    }
}
