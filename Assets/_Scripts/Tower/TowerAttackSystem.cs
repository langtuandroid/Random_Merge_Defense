using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TowerAttackSystem : MonoBehaviour
{
    [SerializeField] protected TowerData towerData;
    [SerializeField] protected Rigidbody parentRigid;
    [SerializeField] SphereCollider attackDetectCollider;
    [SerializeField] protected List<EnemyController> enemyControllers = new List<EnemyController>();
    IEnumerator _attackCo;
    WaitForFixedUpdate waitForFixedUpdate;
    protected System.Action attackAnimation;
    bool critical;
    protected Vector3 playerRotation;
    public void Initialize(TowerData towerData, System.Action attackAnimation)
    {
        attackDetectCollider.enabled = true;
        attackDetectCollider.radius = towerData.AttackDistance;

        enemyControllers.Clear();
        parentRigid.transform.localRotation = Quaternion.identity;
        this.attackAnimation = attackAnimation;
        this.towerData = towerData;
        _attackCo = AttackCo();
        StartCoroutine(_attackCo);
        waitForFixedUpdate = new WaitForFixedUpdate();
    }
    IEnumerator AttackCo()
    {
        while (true)
        {
            while (enemyControllers.Count == 0)
            {
                yield return waitForFixedUpdate;
            }
            yield return StartCoroutine(Operate());
            if (towerData.OperationInterval <= 0)
            {
                yield return new WaitForSeconds(towerData.ActCoolDown);
            }
            else
            {
                yield return new WaitForSeconds(towerData.ActCoolDown - (towerData.OperationInterval * towerData.OperationTimes));
            }
        }
    }

    IEnumerator Operate()
    {
        for (int count = 0; count < towerData.OperationTimes; count++)
        {
            CheckEnemyDeath();
            OrderBy();
            critical = towerData.CriticalRate <= Random.value ? true : false;
            if (enemyControllers.Count != 0)
            {
                for (int attackCount = 0; attackCount < towerData.ObjectMultiple; attackCount++)
                {
                    SetPlayerRotation();
                    attackAnimation.Invoke();
                    Attack(attackCount);
                }
                yield return new WaitForSeconds(towerData.OperationInterval);
            }
        }
    }
    void CheckEnemyDeath()
    {
        for (int i = enemyControllers.Count - 1; i >= 0; i--)
        {
            if (enemyControllers[i].IsDeath)
            {
                enemyControllers.Remove(enemyControllers[i]);
            }
        }
    }
    void OrderBy()
    {
        enemyControllers = enemyControllers.OrderBy(x => x.Order).ToList();
    }
    protected abstract void Attack(int attackCount);


    private void OnTriggerEnter(Collider other)
    {
        enemyControllers.Add(other.GetComponent<EnemyController>());
    }
    private void OnTriggerExit(Collider other)
    {
        enemyControllers.Remove(other.GetComponent<EnemyController>());
    }
    public void DragSet()
    {
        parentRigid.transform.localRotation = Quaternion.identity;
        enemyControllers.Clear();
        attackDetectCollider.enabled = false;
    }
    void SetPlayerRotation()
    {
        playerRotation = enemyControllers[0].Rigidbody.position - parentRigid.position;
        playerRotation.y = 0;
        parentRigid.rotation = Quaternion.LookRotation(playerRotation);
    }
    protected void Attack(EnemyController enemyController)
    {
        if (critical)
        {
            enemyController.DamageReceiver.Damage(towerData.CriticalAttackPower, true);
        }
        else
        {
            enemyController.DamageReceiver.Damage(towerData.AttackPower, false);
        }
    }
}
