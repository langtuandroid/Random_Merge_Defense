using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : SingletonComponent<LifeManager>
{
    [SerializeField] int lifeAmount = 100;
    public void Initialize()
    {

    }
    public void LifeDecrease()
    {
        lifeAmount--;
    }
}
