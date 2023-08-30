using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTile : RuleTile3D
{
    public int SeatId => seatId;
    public bool Filled => filled;
    public TowerController TowerController => towerController;
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
        this.towerController = towerController;
        filledSeatAdd.Invoke(this);
        filled = true;
    }
    public void DeleteTower()
    {
        towerController.Delete();
        notFilledSeatAdd.Invoke(this);
        filled = false;
    }
    public void ChangeTower(TowerController towerController)
    {
        filled = true;
        this.towerController = towerController;
        towerController.transform.position = transform.position;
    }
    public void MoveTower()
    {
        towerController = null;
        notFilledSeatAdd.Invoke(this);
        filled = false;
    }



#if UNITY_EDITOR
    public void Setting(int seatId)
    {
        this.seatId = seatId;
    }
#endif
}
