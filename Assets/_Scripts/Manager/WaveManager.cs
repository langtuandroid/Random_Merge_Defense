using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    EnemySpawnManager enemySpawnManager;
    int currentWaveIndex;
    public void Initialize(int stageId, int currentWaveIndex)
    {
        this.currentWaveIndex = currentWaveIndex;
        enemySpawnManager = GetComponentInChildren<EnemySpawnManager>();
        enemySpawnManager.Initialize(stageId, currentWaveIndex);
    }
    private void Update()
    {

    }

    public void WaveClear()
    {

    }
}
