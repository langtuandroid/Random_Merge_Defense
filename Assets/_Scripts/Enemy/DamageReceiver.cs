using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    protected HPSystem _hPSystem;
    public HPSystem HPSystem
    {
        get
        {
            return _hPSystem;
        }
        set
        {
            _hPSystem = value;
        }
    }
    public bool IsDeath => _hPSystem.IsDeath;
    public virtual void Damage(float damage) => _hPSystem.Damage(damage);
}
