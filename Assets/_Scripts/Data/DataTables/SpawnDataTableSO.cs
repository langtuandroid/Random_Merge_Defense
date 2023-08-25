using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataTable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
[Serializable]
public struct SpawnDataTable
{
    public int stageId;
    public int[] waveIds;
}
namespace DataTable
{
    [CreateAssetMenu(fileName = "SpawnDataTable", menuName = "DataTable/SpawnDataTable", order = 0)]
    public class SpawnDataTableSO : DataTableSO
    {
        [SerializeField] SpawnDataTable[] spawnDataTables;
        public override void Read(string jsonString)
        {
            JArray jArray = JArray.Parse(jsonString);
            for (int i = 0; i < jArray.Count; i++)
            {
                JsonModify(jsonString, "waveIds", i, ref jArray);
            }
            jsonString = jArray.ToString();
#if UNITY_EDITOR
            spawnDataTables = JsonConvert.DeserializeObject<SpawnDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public SpawnDataTable[] GetDataTables()
        {
            return spawnDataTables;
        }
        public int[] GetWavesInStage(int stageId)
        {
            return spawnDataTables.Where(x => x.stageId == stageId).FirstOrDefault().waveIds;
        }
    }
}
