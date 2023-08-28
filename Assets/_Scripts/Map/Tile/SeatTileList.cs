using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTileList : SingletonComponent<SeatTileList>
{
    [SerializeField] SeatTile[] seatTiles;
    public void Initialize()
    {
        List<SeatData> seatData = DataManager.Database.InGameDataLayer.GetData().seatDatas;
        for (int i = 0; i < seatData.Count; i++)
        {
            for (int j = 0; j < seatTiles.Length; j++)
            {
                if (seatData[i].seatId == seatTiles[j].SeatId)
                {
                    seatTiles[j].FillTower(seatData[i].towerId, seatData[i].abilityId);
                }
            }
        }

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
