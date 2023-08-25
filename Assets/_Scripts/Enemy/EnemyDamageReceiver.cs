using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    Rigidbody _rigidbody;
    WaitForFixedUpdate _knockBackWait;
    WaitForSeconds _knockbackDuration;
    System.Action _onBattleMove;
    System.Action<bool> _onKnockBack;
    [SerializeField] float knockbackDuration = 0.5f;
    public void Initialize(float hp, Action deathAction)
    {
        this._onBattleMove = null;
        _hPSystem = new HPSystem(hp, deathAction, null);
        _knockBackWait = new WaitForFixedUpdate();
        _knockbackDuration = new WaitForSeconds(knockbackDuration);
    }
    public override void Damage(float damage)
    {
        base.Damage(damage);
        FactoryManager.Instance.GetFloatingTextParticle(transform.position).Play(string.Format($"{(int)damage}"));
    }


}
