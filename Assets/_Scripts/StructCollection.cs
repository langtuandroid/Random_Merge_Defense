using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [System.Serializable]
    public class StageData
    {
        public int currentStageId;
        public int highestStageId;
        public int currentWaveIndex;
    }
}



public struct EnemyData
{
    public string id;
    public float moveSpeed;
    public float rotateSpeed;
    public float hp;
    public float goldAmount;
    public int lifeDecreaseAmount;
}

