using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UniRx;
using System.Linq;
using Newtonsoft.Json.Linq;

[System.Serializable]
public struct TowerStatusDataTable
{
    public string towerId;
    public string[] grades;
}
namespace DataTable
{
    [CreateAssetMenu(fileName = "TowerStatusDataTable", menuName = "DataTable/TowerStatusDataTable", order = 0)]
    public class TowerStatusDataTableSO : DataTableSO
    {
        [SerializeField] TowerStatusDataTable[] towerStatusDataTable;
        public int MaxGrade => maxGrade;
        [SerializeField] int maxGrade;
        public override void Read(string jsonString)
        {
#if UNITY_EDITOR

            // 배열 형식으로 변경
            JArray jArray = JArray.Parse(jsonString);

            for (int i = 0; i < jArray.Count; i++)
            {
                JsonModify(jsonString, "grades", i, ref jArray);
            }
            jsonString = jArray.ToString();

            towerStatusDataTable = JsonConvert.DeserializeObject<TowerStatusDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
            maxGrade = towerStatusDataTable[0].grades.Length;
#endif
        }
        public TowerStatusDataTable GetTowerStatusDataTable(string towerId)
        {
            return towerStatusDataTable.Where(x => x.towerId == towerId).FirstOrDefault();
        }
        public string GetAbilityId(string towerId, int grade)
        {
            return towerStatusDataTable.Where(x => x.towerId == towerId).FirstOrDefault().grades[grade];
        }

        public TowerStatusDataTable[] GetTables()
        {
            return towerStatusDataTable;
        }
    }
}
