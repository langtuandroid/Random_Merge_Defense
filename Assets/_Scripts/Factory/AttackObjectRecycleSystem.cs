using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AttackObjectRecycleSystem
{

    [SerializeField] RecycleComponent[] attackObjects;
    public Task Initialize()
    {
        List<RecycleComponent> tempAttackObject = new List<RecycleComponent>();


        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>("AttackObject", null);
        handle.Completed += (result) =>
       {
           var results = result.Result;
           for (int i = 0; i < results.Count; i++)
           {
               RecycleObject recycleObject = results[i].GetComponent<RecycleObject>();
               RecycleComponent tempobject = new RecycleComponent();
               tempobject.Initialize(recycleObject);
               tempAttackObject.Add(tempobject);
           }
           attackObjects = tempAttackObject.ToArray();
           //    Addressables.Release(handle);
       };
        return handle.Task;
    }
    public AttackObject GetAttackObject(string Id, Vector3 pos)
    {
        for (int i = 0; i < attackObjects.Length; i++)
        {
            if (Id == attackObjects[i].id)
            {
                AttackObject attackObject = attackObjects[i].factory.Get().GetComponent<AttackObject>();
                attackObject.transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 180, 0));
                return attackObject;
            }
        }
        Debug.LogError(string.Format($"Id 없음 Id = {Id}"));
        return null;
    }
    public void AllRestore()
    {
        int count = attackObjects.Length;
        for (int i = 0; i < count; i++)
        {
            attackObjects[i].AllRestore();
        }
    }
}
