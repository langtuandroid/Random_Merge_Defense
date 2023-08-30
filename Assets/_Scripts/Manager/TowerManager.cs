using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : SingletonComponent<TowerManager>
{
    public enum ClickType { NotThing, FilledSeat, NotFilledSeat }
    ClickType clickType;
    TowerBuildingSystem TowerBuildingSystem => towerBuildingSystem;
    TowerBuildingSystem towerBuildingSystem;
    new Camera camera;
    [SerializeField] float doubleClickCheckTime = 0.2f;
    [SerializeField] float doubleClickTimer = 0;
    [SerializeField] int notFilledDoubleClickCheck;
    [SerializeField] int filledDoubleClickCheck;
    SeatTile clickedSeat;
    SeatTile clickedFilledSeat;
    public void Initialize()
    {
        towerBuildingSystem = GetComponentInChildren<TowerBuildingSystem>();
        camera = CameraManager.Instance.GetComponent<Camera>();
        notFilledDoubleClickCheck = 0;
        filledDoubleClickCheck = 0;

        towerBuildingSystem.Initialize();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickType == ClickType.FilledSeat)
            {
                clickedFilledSeat.TowerController.OffAttackRangeVisual();
            }
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitSeat, 100, AllLayer.SeatLayer))
            {
                SeatTile tempSeatTile = hitSeat.transform.GetComponent<SeatTile>();

                //빈 시트 클릭시
                if (!tempSeatTile.Filled)
                {
                    if (clickType == ClickType.FilledSeat)
                    {
                        clickedFilledSeat.TowerController.OffAttackRangeVisual();
                    }
                    clickType = ClickType.NotFilledSeat;
                    filledDoubleClickCheck = 0;
                    notFilledDoubleClickCheck++;
                    if (notFilledDoubleClickCheck == 1)
                    {
                        doubleClickTimer = 0;
                        clickedSeat = tempSeatTile;
                    }
                    else if (notFilledDoubleClickCheck == 2)
                    {
                        notFilledDoubleClickCheck = 0;
                        doubleClickTimer = 0;
                        if (clickedSeat == tempSeatTile)
                        {
                            towerBuildingSystem.SelectBuildTower(clickedSeat);
                            clickedSeat = null;
                        }
                    }
                }
                //타워가 있는 시트 클릭시
                else
                {
                    clickType = ClickType.FilledSeat;
                    notFilledDoubleClickCheck = 0;
                    filledDoubleClickCheck++;
                    if (filledDoubleClickCheck == 1)
                    {
                        doubleClickTimer = 0;
                        clickedFilledSeat = tempSeatTile;
                        clickedFilledSeat.TowerController.OnAttackRangeVisual();
                    }
                    else if (filledDoubleClickCheck == 2)
                    {
                        doubleClickTimer = 0;
                        filledDoubleClickCheck = 0;
                        clickedFilledSeat.TowerController.OffAttackRangeVisual();
                        if (clickedFilledSeat == tempSeatTile)
                        {
                            towerBuildingSystem.DoubleClickMerge(clickedFilledSeat);
                        }
                    }
                }
            }
            //빈곳 클릭시
            else if (clickType == ClickType.FilledSeat)
            {
                notFilledDoubleClickCheck = 0;
                filledDoubleClickCheck = 0;
                clickType = ClickType.NotThing;
                clickedFilledSeat.TowerController.OffAttackRangeVisual();
            }
        }

        if (notFilledDoubleClickCheck == 1)
        {
            doubleClickTimer += Time.deltaTime;
            if (doubleClickTimer >= doubleClickCheckTime)
            {
                notFilledDoubleClickCheck = 0;
                doubleClickTimer = 0;
            }
        }

        if (filledDoubleClickCheck == 1)
        {
            doubleClickTimer += Time.deltaTime;
            if (doubleClickTimer >= doubleClickCheckTime)
            {
                filledDoubleClickCheck = 0;
                doubleClickTimer = 0;
            }
        }
    }


    public void RandomBuildTower()
    {
        towerBuildingSystem.RandomBuildTower();
    }
}
