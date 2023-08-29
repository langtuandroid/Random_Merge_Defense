using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : SingletonComponent<TowerManager>
{
    TowerBuildingSystem TowerBuildingSystem => towerBuildingSystem;
    TowerBuildingSystem towerBuildingSystem;
    new Camera camera;
    [SerializeField] float doubleClickCheckTime = 0.2f;
    [SerializeField] float doubleClickTimer = 0;
    [SerializeField] int doubleClickCheckCount;
    SeatTile clickedSeat;
    public void Initialize()
    {
        towerBuildingSystem = GetComponentInChildren<TowerBuildingSystem>();
        camera = CameraManager.Instance.GetComponent<Camera>();
        doubleClickCheckCount = 0;

        towerBuildingSystem.Initialize();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitSeat, 100, AllLayer.SeatLayer))
            {
                Debug.Log("히트다히트");
                SeatTile tempSeatTile = hitSeat.transform.GetComponent<SeatTile>();
                if (!tempSeatTile.Filled)
                {
                    doubleClickCheckCount++;
                    if (doubleClickCheckCount == 1)
                    {
                        clickedSeat = tempSeatTile;
                    }
                    else if (doubleClickCheckCount == 2)
                    {
                        doubleClickCheckCount = 0;
                        doubleClickTimer = 0;
                        if (clickedSeat == tempSeatTile)
                        {
                            towerBuildingSystem.SelectBuildTower(clickedSeat);
                            clickedSeat = null;
                        }
                    }
                }
            }
        }
        if (doubleClickCheckCount == 1)
        {
            doubleClickTimer += Time.deltaTime;
            if (doubleClickTimer >= doubleClickCheckTime)
            {
                doubleClickCheckCount = 0;
                doubleClickTimer = 0;
            }
        }
    }


    public void RandomBuildTower()
    {
        towerBuildingSystem.RandomBuildTower();
    }
}
