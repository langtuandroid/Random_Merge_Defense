using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public struct InGame_TowerUpgradeDataTable
{
    public string towerId;
    public float upgradeValue;

}
namespace DataTable
{
    [CreateAssetMenu(fileName = "InGame_TowerUpgradeDataTable", menuName = "DataTable/InGame_TowerUpgradeDataTable", order = 0)]
    public class InGame_TowerUpgradeDataTableSO : DataTableSO
    {
        [SerializeField] private InGame_TowerUpgradeDataTable[] inGame_TowerUpgradeDataTables;
        public override void Read(string jsonString)
        {
#if UNITY_EDITOR

            inGame_TowerUpgradeDataTables = JsonConvert.DeserializeObject<InGame_TowerUpgradeDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public InGame_TowerUpgradeDataTable GetTowerAbilityDataTable(string towerId)
        {
            return inGame_TowerUpgradeDataTables.Where(x => x.towerId == towerId).FirstOrDefault();
        }
        public InGame_TowerUpgradeDataTable[] GetTables()
        {
            return inGame_TowerUpgradeDataTables;
        }
    }
}
