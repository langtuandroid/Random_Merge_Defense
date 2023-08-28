
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnDataProcessing
{
    [System.Serializable]
    public struct EnemySpawnList
    {
        public int waveOrder;
        public List<EnemyGroup> enemyGroups;

        public EnemySpawnList(int waveOrder, List<EnemyGroup> enemyGroups)
        {
            this.waveOrder = waveOrder;
            this.enemyGroups = enemyGroups;
        }
    }
    [System.Serializable]
    public struct EnemyGroup
    {
        public int spawnAmount;
        public float groupSpawnInterval;
        public float enemySpawnInterval;
        public EnemyData enemyData;
        public EnemyGroup(int spawnAmount, float groupSpawnInterval, float enemySpawnInterval, EnemyData enemyData)
        {
            this.spawnAmount = spawnAmount;
            this.groupSpawnInterval = groupSpawnInterval;
            this.enemySpawnInterval = enemySpawnInterval;
            this.enemyData = enemyData;
        }
    }
    public List<EnemySpawnList> enemySpawnLists = new List<EnemySpawnList>();
    public List<EnemySpawnList> EnemySpawnLists => enemySpawnLists;
    public EnemySpawnDataProcessing(int[] waves, int currentWaveOrder)
    {
        //현재 Wave순서부터 생성 시작 -1 제거
        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i] != -1 && i >= currentWaveOrder)
            {
                int waveSpawnId = waves[i];
                SetEnemySpawnList(i, waveSpawnId);
            }
            else if (i < currentWaveOrder)
            {
                SetEmptySpawnList();
            }
        }
    }
    void SetEmptySpawnList()
    {
        enemySpawnLists.Add(new EnemySpawnList(-1, null));
    }
    void SetEnemySpawnList(int order, int waveSpawnId)
    {
        enemySpawnLists.Add(new EnemySpawnList(order, SetEnemyGroup(waveSpawnId)));
    }

    List<EnemyGroup> SetEnemyGroup(int waveSpawnId)
    {
        WaveSpawnDataTable waveSpawnDataTable = DataManager.DataTableBase.WaveSpawnDataTable.GetWaveSpawnData(waveSpawnId);
        List<EnemyGroup> enemyGroupList = new List<EnemyGroup>();
        for (int i = 0; i < waveSpawnDataTable.enemyIds.Length; i++)
        {
            if (waveSpawnDataTable.enemyIds[i] != "-1")
            {
                EnemyDataTable enemyDataTable = DataManager.DataTableBase.EnemyDataTable.GetEnemyData(waveSpawnDataTable.enemyIds[i]);

                enemyGroupList.Add(new EnemyGroup(waveSpawnDataTable.enemyAmounts[i], waveSpawnDataTable.groupSpawnIntervals[i],
                enemyDataTable.spawnInterval, SetEnemyData(waveSpawnDataTable.enemyHpLevel, waveSpawnDataTable.enemyGoldAmounts[i], enemyDataTable)));
            }
        }
        return enemyGroupList;
    }

    EnemyData SetEnemyData(int enemyHpLevel, int enemyGoldAmount, EnemyDataTable enemyDataTable)
    {
        return new EnemyData(enemyDataTable.enemyId, enemyDataTable.moveSpeed, enemyDataTable.rotateSpeed, Pow(enemyDataTable.hp, enemyDataTable.hpGrowth, enemyHpLevel), enemyGoldAmount, enemyDataTable.lifeDecreaseAmount);
    }
    float Pow(float baseValue, float growth, int level)
    {
        float a = 1 + growth;
        float a_cc = a;
        //(baseValue) * (1+growth)^(레벨-1)
        //Pow는 연산속도 느려서 패스!
        if (level == 1)
        {
            return baseValue;
        }

        for (int i = 1; i < level; i++)
        {
            a_cc *= a;
        }
        return baseValue * a_cc;
        // return baseValue * Mathf.Pow(1 + growth, level);
    }
}