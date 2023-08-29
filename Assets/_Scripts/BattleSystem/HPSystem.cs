using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
public class HPSystem
{
    float maxHp;
    FloatReactiveProperty currentHp = new FloatReactiveProperty();
    float _gaugeValue;

    private bool isDeath = false;
    public bool IsDeath => isDeath;
    IDisposable hpSubscribe;
    public HPSystem(float maxHp, Action deathAction, HPBar hPBar)
    {
        this.maxHp = maxHp;
        currentHp.Value = maxHp;
        isDeath = false;
        hPBar.Initialize();
        hpSubscribe = currentHp.Subscribe(x =>
             {
                 if (x <= 0)
                 {
                     isDeath = true;
                     deathAction.Invoke();
                 }
                 _gaugeValue = x / this.maxHp;
                 hPBar.UpdateHPBar(_gaugeValue);
             });
    }
    public void Damage(float damage)
    {
        currentHp.Value -= damage;
        if (isDeath)
        {
            hpSubscribe.Dispose();
        }
    }
}
