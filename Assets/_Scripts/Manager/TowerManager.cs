using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class TowerManager : SingletonComponent<TowerManager>
{
    public enum ClickType { NotThing, FilledSeat, NotFilledSeat, Drag }
    ClickType clickType;
    TowerBuildingSystem TowerBuildingSystem => towerBuildingSystem;
    TowerBuildingSystem towerBuildingSystem;
    new Camera camera;
    public int GoldIncrease => goldIncrease;
    [SerializeField] int goldIncrease = 10;
    [SerializeField] float doubleClickCheckTime = 0.2f;
    [SerializeField] float doubleClickTimer = 0;
    [SerializeField] int notFilledDoubleClickCheck;
    [SerializeField] int filledDoubleClickCheck;
    SeatTile clickedSeat;
    SeatTile clickedFilledSeat;
    Ray ray;
    RaycastHit hitSeat;
    TowerController dragingTower;
    bool onDrag;
    Vector3 touchPosition;
    [SerializeField] float dragCheckDistance = 1;
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
            touchPosition = Input.mousePosition;
            if (clickType == ClickType.FilledSeat)
            {
                clickedFilledSeat.TowerController.OffAttackRangeVisual();
            }
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitSeat, 100, AllLayer.SeatLayer))
            {
                SeatTile tempSeatTile = hitSeat.transform.GetComponent<SeatTile>();

                //빈 시트 클릭시
                if (!tempSeatTile.Filled)
                {
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
                        clickType = ClickType.NotThing;
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
                clickedFilledSeat.TowerController.OffAttackRangeVisual();
            }
        }
        //드래그
        else if (Input.GetMouseButton(0))
        {
            if (clickType == ClickType.FilledSeat)
            {
                if (!onDrag)
                {
                    if ((touchPosition - Input.mousePosition).magnitude >= dragCheckDistance)
                    {
                        clickedFilledSeat.TowerController.OffAttackRangeVisual();
                        dragingTower = FactoryManager.Instance.GetTower(clickedFilledSeat.TowerController.TowerData.TowerID, Vector3.one * 999);
                        dragingTower.DragSet(clickedFilledSeat.TowerController.TowerData.AttackDistance);
                        onDrag = true;
                    }
                }
                else
                {
                    ray = camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hitSeat, 100, AllLayer.DragPointLayer))
                    {
                        dragingTower.transform.position = hitSeat.point;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (clickType == ClickType.FilledSeat && onDrag)
            {
                dragingTower.Delete();
                ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitSeat, 100, AllLayer.SeatLayer))
                {
                    SeatTile mergeSeat = hitSeat.transform.GetComponent<SeatTile>();
                    if (!towerBuildingSystem.DragMerge(mergeSeat, clickedFilledSeat))
                    {
                        if (mergeSeat.Filled)
                        {
                            TowerController tower1 = mergeSeat.TowerController;
                            TowerController tower2 = clickedFilledSeat.TowerController;
                            mergeSeat.ChangeTower(tower2);
                            clickedFilledSeat.ChangeTower(tower1);
                        }
                        else
                        {
                            mergeSeat.ChangeTower(clickedFilledSeat.TowerController);
                            clickedFilledSeat.MoveTower();
                        }
                    }
                    clickType = ClickType.NotThing;
                }
            }
            onDrag = false;
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
