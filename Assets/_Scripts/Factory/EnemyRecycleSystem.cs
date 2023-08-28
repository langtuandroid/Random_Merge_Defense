using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class EnemyRecycleSystem
{

    [SerializeField] RecycleComponent[] enemys;
    public Task Initialize()
    {
        List<RecycleComponent> tempEnemys = new List<RecycleComponent>();


        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>("Enemy", null);
        handle.Completed += (result) =>
       {
           var results = result.Result;
           for (int i = 0; i < results.Count; i++)
           {
               RecycleObject recycleEnemy = results[i].GetComponent<RecycleObject>();
               RecycleComponent tempEnemy = new RecycleComponent();
               tempEnemy.Initialize(recycleEnemy);
               tempEnemys.Add(tempEnemy);
           }
           enemys = tempEnemys.ToArray();
           //    Addressables.Release(handle);
       };
        return handle.Task;
    }
    public EnemyController GetRecycleEnemy(string Id, Vector3 pos, Vector3 lookAtTarget)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (Id == enemys[i].id)
            {
                EnemyController enemyController = enemys[i].factory.Get().GetComponent<EnemyController>();
                enemyController.transform.position = pos;
                enemyController.transform.LookAt(lookAtTarget);
                return enemyController;
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
