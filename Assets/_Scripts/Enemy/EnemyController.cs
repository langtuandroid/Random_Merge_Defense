using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody Rigidbody => enemyMoveSystem.Rigidbody;
    public int Order => enemyData.order;
    public bool IsDeath => isDeath;
    public DamageReceiver DamageReceiver => enemyDamageReceiver;
    bool isDeath;
    [SerializeField] RecycleObject recycleObject;
    EnemyMoveSystem enemyMoveSystem;
    EnemyDamageReceiver enemyDamageReceiver;
    System.Action<EnemyController> removeEnemy;
    [SerializeField] EnemyData enemyData;
    public void Initialize(System.Action<EnemyController> removeEnemy, EnemyData enemyData)
    {
        isDeath = false;
        this.enemyData = enemyData;
        this.removeEnemy = removeEnemy;
        enemyMoveSystem = GetComponentInChildren<EnemyMoveSystem>();
        enemyMoveSystem.Initialize(enemyData, PlayerLifeDecrease);
        enemyDamageReceiver = GetComponentInChildren<EnemyDamageReceiver>();
        enemyDamageReceiver.Initialize(enemyData.hp, Death);
    }
    void Death()
    {
        isDeath = true;
        removeEnemy.Invoke(this);
        recycleObject.Restore();
    }
    void PlayerLifeDecrease()
    {
        removeEnemy.Invoke(this);
        LifeManager.Instance.LifeDecrease(enemyData.lifeDecreaseAmount);
        recycleObject.Restore();
    }

}
