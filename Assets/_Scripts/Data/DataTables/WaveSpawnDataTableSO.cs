using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataTable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
[Serializable]
public struct WaveSpawnDataTable
{
    public int waveSpawnId;
    public int enemyHpLevel;
    public int[] enemyGoldAmounts;
    public string[] enemyIds;
    public float[] groupSpawnIntervals;
    public int[] enemyAmounts;
}
namespace DataTable
{
    [CreateAssetMenu(fileName = "WaveSpawnDataTable", menuName = "DataTable/WaveSpawnDataTable", order = 0)]
    public class WaveSpawnDataTableSO : DataTableSO
    {
        [SerializeField] WaveSpawnDataTable[] waveSpawnDataTable;
        public override void Read(string jsonString)
        {
            JArray jArray = JArray.Parse(jsonString);
            for (int i = 0; i < jArray.Count; i++)
            {
                JsonModify(jsonString, "enemyIds", i, ref jArray);
                JsonModify(jsonString, "enemyGoldAmounts", i, ref jArray);
                JsonModify(jsonString, "groupSpawnIntervals", i, ref jArray);
                JsonModify(jsonString, "enemyAmounts", i, ref jArray);
            }
            jsonString = jArray.ToString();
#if UNITY_EDITOR
            waveSpawnDataTable = JsonConvert.DeserializeObject<WaveSpawnDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public WaveSpawnDataTable[] GetDataTables()
        {
            return waveSpawnDataTable;
        }
        public WaveSpawnDataTable GetWaveSpawnData(int waveSpawnId)
        {
            return waveSpawnDataTable.Where(x => x.waveSpawnId == waveSpawnId).FirstOrDefault();
        }

    }
}
