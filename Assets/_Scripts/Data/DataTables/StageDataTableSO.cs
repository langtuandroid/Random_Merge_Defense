using System.Collections;
using System.Collections.Generic;
using DataTable;
using Newtonsoft.Json;
using UnityEngine;
[System.Serializable]
public struct StageDataTable
{
    public int stageId;
    public string sceneName;
}

namespace DataTable
{
    [CreateAssetMenu(fileName = "StageDataTable", menuName = "DataTable/StageDataTable", order = 0)]
    public class StageDataTableSO : DataTableSO
    {
        [SerializeField] StageDataTable[] stageDataTables;
        public override void Read(string jsonString)
        {
#if UNITY_EDITOR
            stageDataTables = JsonConvert.DeserializeObject<StageDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public StageDataTable[] GetDataTables()
        {
            return stageDataTables;
        }
    }
}
