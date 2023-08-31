using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CannonAttackSystem : TowerAttackSystem
{
    // AttackObject attackObject;
    Tween attackTween;
    Collider[] enemys;
    RecycleParticle recycleParticle;
    [SerializeField] string particleId = "CannonBomb_Particle";
    protected override void Attack(int attackCount)
    {
        parentRigid.rotation = Quaternion.LookRotation(playerRotation);
        AttackObject attackObject = FactoryManager.Instance.GetAttackObject(towerData.TowerID, transform.position);
        attackTween = attackObject.ParabolicAttack(enemyControllers[0].transform.position, towerData.ObjectSpeed);
        attackTween.OnComplete(() =>
        {
            attackObject.Restore();
            enemys = Physics.OverlapSphere(attackObject.transform.position, towerData.Values[0], AllLayer.EnemyLayer);
            recycleParticle = FactoryManager.Instance.GetParticle(particleId, attackObject.transform.position);
            recycleParticle.transform.localScale = Vector3.one * 2 * towerData.Values[0];
            recycleParticle.Play();
            for (int i = 0; i < enemys.Length; i++)
            {
                Attack(enemys[i].GetComponent<EnemyController>());
            }
        }).Play();
    }

}
