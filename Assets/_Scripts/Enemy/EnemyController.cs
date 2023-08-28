using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] RecycleObject recycleObject;
    EnemyMoveSystem enemyMoveSystem;
    EnemyDamageReceiver enemyDamageReceiver;
    System.Action<EnemyController> removeEnemy;
    [SerializeField] EnemyData enemyData;
    public void Initialize(System.Action<EnemyController> removeEnemy, EnemyData enemyData)
    {
        this.enemyData = enemyData;
        this.removeEnemy = removeEnemy;
        enemyMoveSystem = GetComponentInChildren<EnemyMoveSystem>();
        enemyMoveSystem.Initialize(enemyData, PlayerLifeDecrease);
        enemyDamageReceiver = GetComponentInChildren<EnemyDamageReceiver>();
        enemyDamageReceiver.Initialize(enemyData.hp, Death);
    }
    void Death()
    {
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
