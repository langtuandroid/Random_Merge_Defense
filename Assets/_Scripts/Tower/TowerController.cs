using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] TowerData towerData;
    [SerializeField] RecycleObject recycleObject;
    [SerializeField] TowerAttackSystem towerAttackSystem;
    TowerAnimationSystem towerAnimationSystem;
    [SerializeField] GameObject attackRangeVisual;
    public void Initialize(TowerData towerData)
    {
        this.towerData = towerData;
        towerAnimationSystem = new TowerAnimationSystem(GetComponent<Animator>());

        towerAttackSystem.Initialize(this.towerData, towerAnimationSystem.AttackPlay);

        attackRangeVisual.transform.localScale = Vector3.one * towerData.AttackDistance * 2;
        OffAttackRangeVisual();
    }
    public void OnAttackRangeVisual()
    {
        attackRangeVisual.gameObject.SetActive(true);
    }
    public void OffAttackRangeVisual()
    {
        attackRangeVisual.gameObject.SetActive(false);
    }
}
