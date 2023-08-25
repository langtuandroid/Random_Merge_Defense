using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public struct WaveDataTable
{
    public int waveId;
    public int enemyHpLevel;
    public int enemyGoldLevel;
    public List<string> enemyIds;
    public List<float> spawnIntervals;
    public List<int> enemyAmounts;
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
                JsonModify(jsonString, "enemyIds", i, ref jArray);
                JsonModify(jsonString, "spawnIntervals", i, ref jArray);
                JsonModify(jsonString, "enemyAmounts", i, ref jArray);
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
        public WaveDataTable GetWaveDataTable(int waveId)
        {
            return waveDataTables.Where(x => x.waveId == waveId).FirstOrDefault();
        }
    }
}
