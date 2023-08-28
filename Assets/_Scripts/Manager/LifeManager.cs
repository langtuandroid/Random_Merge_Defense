using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : SingletonComponent<LifeManager>
{
    [SerializeField] int lifeAmount = 100;
    public int LifeAmount => lifeAmount;
    public void Initialize()
    {

    }
    public void LifeDecrease(int decreaseAmount)
    {
        lifeAmount -= decreaseAmount;
        InGameUI.Instance.TopUI.SetLifeText(lifeAmount);
    }
}
