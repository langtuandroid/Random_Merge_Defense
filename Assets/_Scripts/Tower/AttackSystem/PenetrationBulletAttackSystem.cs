using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationBulletAttackSystem : TowerAttackSystem
{
    PenetrationBulletAttackObject penetrationBulletAttackObject;
    protected override void Attack(int attackCount)
    {
        penetrationBulletAttackObject = FactoryManager.Instance.GetAttackObject(towerData.TowerID, transform.position) as PenetrationBulletAttackObject;
        Debug.Log(penetrationBulletAttackObject.name);
        penetrationBulletAttackObject.Initialize(playerRotation, transform.position, towerData.ObjectSpeed, towerData.Values[0], Attack);
    }
}
