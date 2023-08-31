using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public struct TowerAbilityDataTable
{
    public string abilityId;
    public int grade;
    public float attackPower;
    public float attackDistance;
    public float criticalRate;
    public float actCoolDown;
    public int operationTimes;
    public float operationInterval;
    public int objectMultiple;
    public float objectMultipleAngle;
    public float objectSpeed;
    public int penetrationCount;
    public float[] values;

}
namespace DataTable
{
    [CreateAssetMenu(fileName = "TowerAbilityDataTable", menuName = "DataTable/TowerAbilityDataTable", order = 0)]
    public class TowerAbilityDataTableSO : DataTableSO
    {
        [SerializeField] private TowerAbilityDataTable[] towerAbilityDataTable;
        public override void Read(string jsonString)
        {
#if UNITY_EDITOR

            // 배열 형식으로 변경
            JArray jArray = JArray.Parse(jsonString);

            for (int i = 0; i < jArray.Count; i++)
            {
                JsonModify(jsonString, "values", i, ref jArray);
            }

            jsonString = jArray.ToString();

            towerAbilityDataTable = JsonConvert.DeserializeObject<TowerAbilityDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public TowerAbilityDataTable GetTowerAbilityDataTable(string abilityId)
        {
            return towerAbilityDataTable.Where(x => x.abilityId == abilityId).FirstOrDefault();
        }
        public TowerAbilityDataTable[] GetTables()
        {
            return towerAbilityDataTable;
        }
    }
}
