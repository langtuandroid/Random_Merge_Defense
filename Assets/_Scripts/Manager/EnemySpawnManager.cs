using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : SingletonComponent<EnemySpawnManager>
{
    public List<EnemyController> EnemyControllers => enemyControllers;

    [SerializeField] float waveInterval = 10f;
    [SerializeField] EnemySpawnDataProcessing spawnDataProcessing;


    List<EnemyController> enemyControllers = new List<EnemyController>();
    bool waveEnd;
    bool allWaveEnd;
    int currentGroupOrder;
    int currentWaveOrder;
    int currentSpawnAmount;
    float enemySpawnIntervalTimer;
    float groupSpawnIntervalTimer;
    float waveIntervalTimer;

    public void Initialize(int stageId, int currentWaveOrder)
    {
        this.currentWaveOrder = currentWaveOrder;
        int[] waves = DataManager.DataTableBase.WaveDataTable.GetCurrentStageWaves(stageId);
        spawnDataProcessing = new EnemySpawnDataProcessing(waves, currentWaveOrder);
        enemySpawnIntervalTimer = spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].enemySpawnInterval;
        groupSpawnIntervalTimer = spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].groupSpawnInterval;
        allWaveEnd = false;
    }

    private void Update()
    {
        if (!waveEnd)
        {
            return;
        }
        //웨이브 시작 전 인터벌
        if (waveIntervalTimer >= waveInterval)
        {
            //웨이브 그룹 인터벌
            if (groupSpawnIntervalTimer >= spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].groupSpawnInterval)
            {
                //웨이브 그룹의 적 인터벌
                if (enemySpawnIntervalTimer >= spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].enemySpawnInterval)
                {
                    EnemyController enemyController = FactoryManager.Instance.GetEnemyController(spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].enemyData.id, PathTileList.Instance.PathPositions[0], PathTileList.Instance.PathPositions[1]);
                    enemyController.Initialize(x => WaveEndCheck(x), spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].enemyData);
                    enemySpawnIntervalTimer = 0;
                    enemyControllers.Add(enemyController);
                    currentSpawnAmount++;

                    //웨이브 그룹의 적 스폰이 끝났다면 
                    if (currentSpawnAmount == spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups[currentGroupOrder].spawnAmount)
                    {
                        currentGroupOrder++;
                        //웨이브 그룹이 끝났다면 
                        if (currentGroupOrder == spawnDataProcessing.EnemySpawnLists[currentWaveOrder].enemyGroups.Count)
                        {
                            waveIntervalTimer = 0;
                            currentGroupOrder = 0;
                            currentWaveOrder++;
                            waveEnd = false;
                            //모든 웨이브의 스폰이 끝났다면
                            if (currentWaveOrder == spawnDataProcessing.EnemySpawnLists.Count - 1)
                            {
                                allWaveEnd = true;
                                enabled = false;
                                return;
                            }
                        }
                        currentSpawnAmount = 0;
                    }

                }
                else
                {
                    enemySpawnIntervalTimer += Time.deltaTime;
                }
            }
            else
            {
                groupSpawnIntervalTimer += Time.deltaTime;
            }
        }
        else
        {
            waveIntervalTimer += Time.deltaTime;
            InGameUI.Instance.WaveUI.SetWaveIntervalText(waveInterval - waveIntervalTimer);

            if (waveIntervalTimer >= waveInterval)
            {
                InGameUI.Instance.WaveUI.Off();
            }
        }
    }
    void WaveEndCheck(EnemyController enemy)
    {
        enemyControllers.Remove(enemy);
        if (!allWaveEnd)
        {
            if (enemyControllers.Count == 0 && !waveEnd)
            {
                InGameUI.Instance.WaveUI.On();
                waveEnd = true;
            }
        }
    }
    public void WaveStartImmediately()
    {
        waveEnd = true;
        waveIntervalTimer = 9999;
    }
}
