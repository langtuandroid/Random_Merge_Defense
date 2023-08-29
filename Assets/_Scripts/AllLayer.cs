using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllLayer
{
    public static int SeatLayer;

    public static void Initialize()
    {
        SeatLayer = 1 << LayerMask.NameToLayer("Seat");
    }
}
