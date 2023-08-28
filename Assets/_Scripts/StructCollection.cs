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
        public int currentWaveOrder;
    }
}


[System.Serializable]
public struct EnemyData
{
    public string id;
    public float moveSpeed;
    public float rotateSpeed;
    public float hp;
    public float goldAmount;
    public int lifeDecreaseAmount;
    public EnemyData(string id, float moveSpeed, float rotateSpeed, float hp, float goldAmount, int lifeDecreaseAmount)
    {
        this.id = id;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.hp = hp;
        this.goldAmount = goldAmount;
        this.lifeDecreaseAmount = lifeDecreaseAmount;

    }
}

