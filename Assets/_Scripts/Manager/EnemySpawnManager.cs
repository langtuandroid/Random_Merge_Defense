using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] EnemySpawnDataProcessing spawnDataProcessing;

    public void Initialize(int stageId, int currentWaveIndex)
    {
        int[] waves = DataManager.DataTableBase.SpawnDataTable.GetWavesInStage(stageId);

        spawnDataProcessing = new EnemySpawnDataProcessing();
    }
}
