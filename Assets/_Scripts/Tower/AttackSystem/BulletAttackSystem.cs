using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackSystem : TowerAttackSystem
{
    Vector3 dir;
    protected override void Attack(int attackCount)
    {
        attackAnimation.Invoke();
        dir = enemyControllers[0].Rigidbody.position - parentRigid.position;
        dir.y = 0;
        parentRigid.rotation = Quaternion.LookRotation(dir);
        if (critical)
        {
            enemyControllers[0].DamageReceiver.Damage(towerData.CriticalAttackPower, true);
        }
        else
        {
            enemyControllers[0].DamageReceiver.Damage(towerData.AttackPower, false);
        }
    }
}
