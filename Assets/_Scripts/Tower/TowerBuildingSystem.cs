using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
public class TowerBuildingSystem : MonoBehaviour
{
    public void Initialize()
    {
        SeatTile[] seatTiles = SeatTileList.Instance.SeatTiles;
        List<SeatData> seatData = DataManager.Database.InGameDataLayer.GetData().seatDatas;
        for (int i = 0; i < seatData.Count; i++)
        {
            for (int j = 0; j < seatTiles.Length; j++)
            {
                if (seatData[i].seatId == seatTiles[j].SeatId)
                {
                    BuildTower(seatData[i].towerId, seatData[i].abilityId, seatTiles[j]);
                }
            }
        }
    }


    public void RandomBuildTower()
    {
        SeatTile[] buildAbleSeats = SeatTileList.Instance.NotFilledSeats.ToArray();
        if (buildAbleSeats.Length == 0) return;

        string[] ownDeckTowerIds = DataManager.Database.PlayerDataLayer.GetData().ownDeckTowerIds.ToArray();
        int randSeatIndex = Random.Range(0, buildAbleSeats.Length);
        int randTowerIndex = Random.Range(0, ownDeckTowerIds.Length);
        string abilityId = DataManager.DataTableBase.TowerStatusDataTable.GetAbilityId(ownDeckTowerIds[randTowerIndex], 0);
        BuildTower(ownDeckTowerIds[randTowerIndex], abilityId, buildAbleSeats[randSeatIndex]);
    }

    public void SelectBuildTower(SeatTile seatTile)
    {
        string[] ownDeckTowerIds = DataManager.Database.PlayerDataLayer.GetData().ownDeckTowerIds.ToArray();
        int randTowerIndex = Random.Range(0, ownDeckTowerIds.Length);
        string abilityId = DataManager.DataTableBase.TowerStatusDataTable.GetAbilityId(ownDeckTowerIds[randTowerIndex], 0);
        BuildTower(ownDeckTowerIds[randTowerIndex], abilityId, seatTile);
    }

    void BuildTower(string towerId, string abilityId, SeatTile seatTile)
    {
        TowerController towerController = FactoryManager.Instance.GetTower(towerId, seatTile.transform.position);
        towerController.Initialize(SetTowerData(towerId, abilityId));
        seatTile.BuildTower(towerController);
    }
    TowerData SetTowerData(string towerId, string abilityId)
    {
        TowerAbilityDataTable towerAbilityDataTable = DataManager.DataTableBase.TowerAbilityDataTable.GetTowerAbilityDataTable(abilityId);
        InGameTowerUpgrade inGame_TowerUpgradeManager = InGame_TowerUpgradeManager.Instance.GetInGameTowerUpgrade(towerId);
        return new TowerData(towerId, towerAbilityDataTable.grade, abilityId, towerAbilityDataTable.attackPower, towerAbilityDataTable.attackDistance, towerAbilityDataTable.criticalRate, towerAbilityDataTable.actCoolDown, towerAbilityDataTable.operationTimes, towerAbilityDataTable.operationInterval, towerAbilityDataTable.objectMultiple, towerAbilityDataTable.objectMultipleAngle, towerAbilityDataTable.penetrationCount, towerAbilityDataTable.values, inGame_TowerUpgradeManager);
    }
    public void DoubleClickMerge(SeatTile mergeSeat)
    {
        List<SeatTile> filledSeats = SeatTileList.Instance.FilledSeats;
        for (int i = 0; i < filledSeats.Count; i++)
        {
            if (mergeSeat.TowerController.TowerData.Grade != DataManager.DataTableBase.TowerStatusDataTable.MaxGrade &&
            filledSeats[i] != mergeSeat &&
             filledSeats[i].TowerController.TowerData.TowerID == mergeSeat.TowerController.TowerData.TowerID &&
             filledSeats[i].TowerController.TowerData.Grade == mergeSeat.TowerController.TowerData.Grade
             )
            {
                TowerData mergeSeatTowerData = mergeSeat.TowerController.TowerData;
                string nextAbilityId = DataManager.DataTableBase.TowerStatusDataTable.GetAbilityId(mergeSeatTowerData.TowerID, mergeSeatTowerData.Grade + 1);
                Debug.Log(mergeSeatTowerData.Grade + 1);
                filledSeats[i].DeleteTower();
                mergeSeat.DeleteTower();
                BuildTower(mergeSeatTowerData.TowerID, nextAbilityId, mergeSeat);
                return;
            }
        }
    }
    public void DragMerge(SeatTile mergeSeat, SeatTile dragedSeat)
    {

        if (mergeSeat.Filled &&
        mergeSeat.TowerController.TowerData.Grade != DataManager.DataTableBase.TowerStatusDataTable.MaxGrade &&
                   dragedSeat != mergeSeat &&
                    dragedSeat.TowerController.TowerData.TowerID == mergeSeat.TowerController.TowerData.TowerID &&
                    dragedSeat.TowerController.TowerData.Grade == mergeSeat.TowerController.TowerData.Grade
        )
        {
            TowerData mergeSeatTowerData = mergeSeat.TowerController.TowerData;
            string nextAbilityId = DataManager.DataTableBase.TowerStatusDataTable.GetAbilityId(mergeSeatTowerData.TowerID, mergeSeatTowerData.Grade + 1);
            Debug.Log(mergeSeatTowerData.Grade + 1);
            dragedSeat.DeleteTower();
            mergeSeat.DeleteTower();
            BuildTower(mergeSeatTowerData.TowerID, nextAbilityId, mergeSeat);
            return;
        }
    }
}
