using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackSystem : TowerAttackSystem
{
    private Collider[] enemys;
    RecycleParticle recycleParticle;
    private string particleId = "RangeAttack_Particle";

    protected override void Attack(int attackCount)
    {
        enemys = Physics.OverlapSphere(transform.position, towerData.AttackDistance, AllLayer.EnemyLayer);
        for (int i = 0; i < enemys.Length; i++)
        {
            Attack(enemys[i].GetComponent<EnemyController>());
        }
        recycleParticle = FactoryManager.Instance.GetParticle(particleId, transform.position);
        recycleParticle.transform.localScale = Vector3.one * towerData.AttackDistance * 2;
        recycleParticle.Play();
    }
}
