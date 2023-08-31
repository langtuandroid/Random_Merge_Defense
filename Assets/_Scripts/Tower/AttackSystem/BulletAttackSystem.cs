using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackSystem : TowerAttackSystem
{

    protected override void Attack(int attackCount)
    {

        Attack(enemyControllers[0]);
    }
}
