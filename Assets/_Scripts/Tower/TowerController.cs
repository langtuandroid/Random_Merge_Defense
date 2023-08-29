using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] TowerData towerData;
    [SerializeField] RecycleObject recycleObject;
    [SerializeField] TowerAttackSystem towerAttackSystem;
    TowerAnimationSystem towerAnimationSystem;
    public void Initialize(TowerData towerData)
    {
        this.towerData = towerData;
        towerAnimationSystem = new TowerAnimationSystem(GetComponent<Animator>());


        towerAttackSystem = GetComponentInChildren<TowerAttackSystem>();
        towerAttackSystem.Initialize(this.towerData, towerAnimationSystem.AttackPlay);
    }
}
