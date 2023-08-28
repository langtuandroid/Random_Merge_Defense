using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTile : RuleTile3D
{
    public int SeatId => seatId;
    public bool Filled => filled;
    [SerializeField] int seatId;
    bool filled;
    TowerController towerController;
    public void FillTower(string towerId, string abilityId)
    {
        filled = true;
        GenerateTower(towerId, abilityId);
    }
    public void DeleteTower()
    {

    }


    void GenerateTower(string towerId, string abilityId)
    {
        towerController = FactoryManager.Instance.GetTower(towerId, transform.position);
        towerController.Initialize(SetTowerData(towerId, abilityId));
    }
    TowerData SetTowerData(string towerId, string abilityId)
    {
        TowerAbilityDataTable towerAbilityDataTable = DataManager.DataTableBase.TowerAbilityDataTable.GetTowerAbilityDataTable(abilityId);
        InGameTowerUpgrade inGame_TowerUpgradeManager = InGame_TowerUpgradeManager.Instance.GetInGameTowerUpgrade(towerId);
        return new TowerData(towerId, abilityId, towerAbilityDataTable.attackPower, towerAbilityDataTable.attackDistance, towerAbilityDataTable.criticalRate, towerAbilityDataTable.actCoolDown, towerAbilityDataTable.operationTimes, towerAbilityDataTable.operationInterval, towerAbilityDataTable.objectMultiple, towerAbilityDataTable.objectMultipleAngle, towerAbilityDataTable.penetrationCount, towerAbilityDataTable.values, inGame_TowerUpgradeManager);
    }









#if UNITY_EDITOR
    public void Setting(int seatId)
    {
        this.seatId = seatId;
    }
#endif
}
