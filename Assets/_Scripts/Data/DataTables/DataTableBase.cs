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
