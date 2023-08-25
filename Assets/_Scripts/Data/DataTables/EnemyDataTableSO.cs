using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public struct EnemyDataTable
{
    public string enemyId;
    public float moveSpeed;
    public float rotateSpeed;
    public float hp;
    public float hpGrowth;
    public float spawnInterval;
    public int goldAmount;
    public int lifeDecreaseAmount;
}
namespace DataTable
{
    [CreateAssetMenu(fileName = "EnemyDataTable", menuName = "DataTable/EnemyDataTable", order = 0)]
    public class EnemyDataTableSO : DataTableSO
    {
        [SerializeField] EnemyDataTable[] _enemyDataTable;
        public override void Read(string jsonString)
        {
#if UNITY_EDITOR
            _enemyDataTable = JsonConvert.DeserializeObject<EnemyDataTable[]>(jsonString);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public EnemyDataTable[] GetDataTables()
        {
            return _enemyDataTable;
        }
    }
}
