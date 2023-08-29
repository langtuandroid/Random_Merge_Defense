using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class TowerRecycleSystem
{

    [SerializeField] RecycleComponent[] enemys;
    public Task Initialize()
    {
        List<RecycleComponent> tempTowers = new List<RecycleComponent>();


        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>("Tower", null);
        handle.Completed += (result) =>
       {
           var results = result.Result;
           for (int i = 0; i < results.Count; i++)
           {
               RecycleObject recycleTower = results[i].GetComponent<RecycleObject>();
               RecycleComponent tempTower = new RecycleComponent();
               tempTower.Initialize(recycleTower);
               tempTowers.Add(tempTower);
           }
           enemys = tempTowers.ToArray();
           //    Addressables.Release(handle);
       };
        return handle.Task;
    }
    public TowerController GetTower(string Id, Vector3 pos)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (Id == enemys[i].id)
            {
                TowerController towerController = enemys[i].factory.Get().GetComponent<TowerController>();
                towerController.transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 180, 0));
                return towerController;
            }
        }
        Debug.LogError(string.Format($"Id 없음 Id = {Id}"));
        return null;
    }
    public void AllRestore()
    {
        int count = enemys.Length;
        for (int i = 0; i < count; i++)
        {
            enemys[i].AllRestore();
        }
    }
}
