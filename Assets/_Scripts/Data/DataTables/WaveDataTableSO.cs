using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public struct WaveDataTable
{
    public int stageId;
    public int[] waveSpawnIds;
}
namespace DataTable
{
    [CreateAssetMenu(fileName = "WaveDataTable", menuName = "DataTable/WaveDataTable", order = 0)]
    public class WaveDataTableSO : DataTableSO
    {
        [SerializeField] WaveDataTable[] waveDataTables;
        public override void Read(string jsonString)
        {
            JArray jArray = JArray.Parse(jsonString);
            for (int i = 0; i < jArray.Count; i++)
            {
                JsonModify(jsonString, "waveSpawnIds", i, ref jArray);
            }
            jsonString = jArray.ToString();
#if UNITY_EDITOR
            waveDataTables = JsonConvert.DeserializeObject<WaveDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public WaveDataTable[] GetDataTables()
        {
            return waveDataTables;
        }
        public int[] GetCurrentStageWaves(int stageId)
        {
            return waveDataTables.Where(x => x.stageId == stageId).FirstOrDefault().waveSpawnIds;
        }
    }
}
