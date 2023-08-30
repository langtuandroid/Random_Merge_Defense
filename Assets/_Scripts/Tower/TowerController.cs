using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public TowerData TowerData => towerData;
    [SerializeField] TowerData towerData;
    [SerializeField] RecycleObject recycleObject;
    [SerializeField] TowerAttackSystem towerAttackSystem;
    TowerAnimationSystem towerAnimationSystem;
    [SerializeField] GameObject attackRangeVisual;
    [SerializeField] TextMeshPro gradeText;
    public void Initialize(TowerData towerData)
    {
        this.towerData = towerData;
        towerAnimationSystem = new TowerAnimationSystem(GetComponent<Animator>());

        towerAttackSystem.Initialize(this.towerData, towerAnimationSystem.AttackPlay);

        attackRangeVisual.transform.localScale = Vector3.one * towerData.AttackDistance * 2;
        OffAttackRangeVisual();
        gradeText.text = string.Format($"{towerData.Grade}");
        gradeText.transform.rotation = CameraManager.Instance.transform.rotation;
    }
    public void OnAttackRangeVisual()
    {
        attackRangeVisual.gameObject.SetActive(true);
    }
    public void OffAttackRangeVisual()
    {
        attackRangeVisual.gameObject.SetActive(false);
    }
    public void Delete()
    {
        recycleObject.Restore();
    }
}
