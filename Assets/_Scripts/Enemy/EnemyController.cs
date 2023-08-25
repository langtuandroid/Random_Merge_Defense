using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyMoveSystem enemyMoveSystem;
    System.Action lastEnemyInWave;
    public void Initialize(System.Action lastEnemyInWave)
    {
        lastEnemyInWave = this.lastEnemyInWave;
        enemyMoveSystem = GetComponentInChildren<EnemyMoveSystem>();
        enemyMoveSystem.Initialize(PlayerLifeDecrease);
    }
    void Death()
    {
        lastEnemyInWave?.Invoke();
        gameObject.SetActive(false);
    }
    void PlayerLifeDecrease()
    {
        lastEnemyInWave?.Invoke();
        LifeManager.Instance.LifeDecrease();
        gameObject.SetActive(false);
    }

}
