using System.Linq;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using DataTable;
public class DataTableBase
{
    DataTableSO[] datatables;
    public EnemyDataTableSO EnemyDataTable => GetTable<EnemyDataTableSO>();
    public WaveSpawnDataTableSO WaveSpawnDataTable => GetTable<WaveSpawnDataTableSO>();
    public StageDataTableSO StageDataTable => GetTable<StageDataTableSO>();
    public WaveDataTableSO WaveDataTable => GetTable<WaveDataTableSO>();
    public TowerAbilityDataTableSO TowerAbilityDataTable => GetTable<TowerAbilityDataTableSO>();
    public TowerStatusDataTableSO TowerStatusDataTable => GetTable<TowerStatusDataTableSO>();
    public InGame_TowerUpgradeDataTableSO InGame_TowerUpgradeDataTable => GetTable<InGame_TowerUpgradeDataTableSO>();
    public async Task Init()
    {
        AsyncOperationHandle<IList<DataTableSO>> handle = Addressables.LoadAssetsAsync<DataTableSO>("DataTable", null);
        await handle.Task;
        datatables = handle.Result.ToArray();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(handle);
        }
    }

    private T GetTable<T>() where T : class
    {
        for (int i = 0; i < datatables.Length; i++)
        {
            T table = datatables[i].Get<T>();
            if (table != null)
            {
                return table;
            }
        }
        return default;
    }
}
