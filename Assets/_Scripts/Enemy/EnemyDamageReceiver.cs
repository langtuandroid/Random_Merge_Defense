using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    public void Initialize(float hp, Action deathAction)
    {
        _hPSystem = new HPSystem(hp, deathAction, GetComponentInChildren<HPBar>());
    }
    public override void Damage(float damage)
    {
        base.Damage(damage);
        FactoryManager.Instance.GetFloatingTextParticle(transform.position).Play(string.Format($"{(int)damage}"));
    }


}
