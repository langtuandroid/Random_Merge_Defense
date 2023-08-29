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
    System.Action<SeatTile> filledSeatAdd;
    System.Action<SeatTile> notFilledSeatAdd;
    public void Initialize(System.Action<SeatTile> filledSeatAdd, System.Action<SeatTile> notFilledSeatAdd)
    {
        this.filledSeatAdd = filledSeatAdd;
        this.notFilledSeatAdd = notFilledSeatAdd;
    }
    public void BuildTower(TowerController towerController)
    {
        filled = true;
        this.towerController = towerController;
        filledSeatAdd.Invoke(this);
    }
    public void DeleteTower()
    {
        notFilledSeatAdd.Invoke(this);
    }





#if UNITY_EDITOR
    public void Setting(int seatId)
    {
        this.seatId = seatId;
    }
#endif
}
