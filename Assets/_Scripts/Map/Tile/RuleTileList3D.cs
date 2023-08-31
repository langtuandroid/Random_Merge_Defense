using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RuleTileList3D : MonoBehaviour
{
    public float startHeight;
    public Dictionary<Vector3, GameObject> ruleTileDict = new Dictionary<Vector3, GameObject>();
    public List<GameObject> tilemapLevels = new List<GameObject>();

    public void InitializeRuleTileList()
    {
        ruleTileDict = new Dictionary<Vector3, GameObject>();
        GetComponentInChildren<PathTileList>().Setting();
        GetComponentInChildren<SeatTileList>().Setting();
    }

    public void CleanTilemapList()
    {
        for (int i = 0; i < tilemapLevels.Count; i++)
        {
            if (tilemapLevels[i].TryGetComponent(out PathTileList pathTileList))
            {
                pathTileList.Setting();
            }
            if (tilemapLevels[i].TryGetComponent(out SeatTileList seatTileList))
            {
                seatTileList.Setting();
            }

            if (tilemapLevels[i] == null)
            {
                tilemapLevels.RemoveAt(i);
                CleanTilemapList();
                return;
            }
            else
            {
                Grid grid = GetComponent<Grid>();
                float level_height = i * grid.cellSize.y * grid.transform.localScale.y;
                tilemapLevels[i].transform.position = new Vector3(0, level_height + startHeight, 0);
            }
        }
    }
}
