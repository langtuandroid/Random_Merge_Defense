using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTileList : SingletonComponent<SeatTileList>
{
    public SeatTile[] SeatTiles => seatTiles;
    [SerializeField] SeatTile[] seatTiles;
    public void Initialize()
    {

    }














#if UNITY_EDITOR
    public void Setting()
    {
        seatTiles = GetComponentsInChildren<SeatTile>();
        for (int i = 0; i < seatTiles.Length; i++)
        {
            seatTiles[i].Setting(i);
        }
    }
#endif
}
