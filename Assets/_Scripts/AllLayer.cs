using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllLayer
{
    public static int SeatLayer;
    public static int EnemyLayer;
    public static int DragPointLayer;
    public static void Initialize()
    {
        SeatLayer = 1 << LayerMask.NameToLayer("Seat");
        EnemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        DragPointLayer = 1 << LayerMask.NameToLayer("DragPoint");
    }
}
