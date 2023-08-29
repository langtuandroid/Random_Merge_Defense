using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTileList : SingletonComponent<SeatTileList>
{
    public SeatTile[] SeatTiles => seatTiles;
    public List<SeatTile> FilledSeats => filledSeats;
    public List<SeatTile> NotFilledSeats => notFilledSeats;
    [SerializeField] SeatTile[] seatTiles;
    [SerializeField] List<SeatTile> filledSeats = new List<SeatTile>();
    [SerializeField] List<SeatTile> notFilledSeats = new List<SeatTile>();
    public void Initialize()
    {
        for (int i = 0; i < seatTiles.Length; i++)
        {
            seatTiles[i].Initialize(filledSeat => FilledSeatAdd(filledSeat), notFilledSeat => NotFilledSeatAdd(notFilledSeat));
            notFilledSeats.Add(seatTiles[i]);
        }
    }
    void FilledSeatAdd(SeatTile seatTile)
    {
        filledSeats.Add(seatTile);
        notFilledSeats.Remove(seatTile);
    }
    void NotFilledSeatAdd(SeatTile seatTile)
    {
        notFilledSeats.Add(seatTile);
        filledSeats.Remove(seatTile);
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
