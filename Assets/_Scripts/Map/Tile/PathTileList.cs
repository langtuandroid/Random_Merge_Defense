using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PathTileList : SingletonComponent<PathTileList>
{
    [SerializeField] Vector3[] pathPositions;
    public Vector3[] PathPositions => pathPositions;
    public void Setting()
    {
        var temtpathTiles = GetComponentsInChildren<PathTile>().Where(x => x.Index >= 0).OrderBy(x => x.Index).ToList();
        PathTile[] pathTiles = temtpathTiles.ToArray();
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < pathTiles.Length; i++)
        {
            pathTiles[i].name = string.Format($"Path {i}");
            positions.Add(pathTiles[i].transform.position);
        }
        pathPositions = positions.ToArray();
    }
}
