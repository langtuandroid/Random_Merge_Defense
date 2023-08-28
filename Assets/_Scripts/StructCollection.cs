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
    [System.Serializable]
    public class PlayerData
    {
        public List<string> ownDeckTowerIds = new List<string>();
    }

    [System.Serializable]
    public class InGameData
    {

        public int goldAmount;
        public List<InGameTowerUpgrade> inGameTowerUpgrades = new List<InGameTowerUpgrade>();
        public List<SeatData> seatDatas = new List<SeatData>();
    }
}

[System.Serializable]
public struct SeatData
{
    public int seatId;
    public string towerId;
    public string abilityId;
}
public class InGameTowerUpgrade
{
    public string towerId;
    public int upgradeLevel;
    float value;
    public float UpgradeValue => 1 + (value * upgradeLevel);
    public InGameTowerUpgrade(string towerId, int upgradeLevel, float value)
    {
        this.towerId = towerId;
        this.upgradeLevel = upgradeLevel;
        this.value = value;
    }
    public void Upgrade()
    {
        upgradeLevel++;
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
[System.Serializable]
public struct TowerData
{
    public string TowerID => towerId;
    public string AbilityId => abilityId;
    public float AttackPower => attackPower * inGameTowerUpgrade.UpgradeValue;
    public float AttackDistance => attackDistance;
    public float CriticalRate => criticalRate;
    public float ActCoolDown => actCoolDown;
    public int OperationTimes => operationTimes;
    public float OperationInterval => operationInterval;
    public float ObjectMultiple => objectMultiple;
    public float ObjectMultipleAngle => objectMultipleAngle;
    public int PenetrationCount => penetrationCount;
    public float[] Values => values;

    private string towerId;
    private string abilityId;
    private float attackPower;
    private float attackDistance;
    private float criticalRate;
    private float actCoolDown;
    private int operationTimes;
    private float operationInterval;
    private float objectMultiple;
    private float objectMultipleAngle;
    private int penetrationCount;
    private float[] values;
    private InGameTowerUpgrade inGameTowerUpgrade;
    public TowerData(string towerId, string abilityId, float attackPower, float attackDistance, float criticalRate, float actCoolDown, int operationTimes, float operationInterval, float objectMultiple, float objectMultipleAngle, int penetrationCount, float[] values, InGameTowerUpgrade inGameTowerUpgrade)
    {
        this.towerId = towerId;
        this.abilityId = abilityId;
        this.attackPower = attackPower;
        this.attackDistance = attackDistance;
        this.criticalRate = criticalRate;
        this.actCoolDown = actCoolDown;
        this.operationTimes = operationTimes;
        this.operationInterval = operationInterval;
        this.objectMultiple = objectMultiple;
        this.objectMultipleAngle = objectMultipleAngle;
        this.penetrationCount = penetrationCount;
        this.values = values;
        this.inGameTowerUpgrade = inGameTowerUpgrade;
    }
}
