using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTile : RuleTile3D
{
    public int SeatId => seatId;
    public bool Filled => filled;
    [SerializeField] int seatId;
    bool filled = false;
    TowerController towerController;
    public void BuildTower(TowerController towerController)
    {
        filled = true;
        this.towerController = towerController;
    }
    public void DeleteTower()
    {

    }





#if UNITY_EDITOR
    public void Setting(int seatId)
    {
        this.seatId = seatId;
    }
#endif
}
