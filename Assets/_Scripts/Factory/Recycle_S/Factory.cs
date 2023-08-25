using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct RecycleComponent
{
    public string id;
    public Factory factory;
    public void Initialize(RecycleObject recycleObject)
    {
        id = recycleObject.id;
        factory = new Factory(recycleObject, recycleObject.poolSize);
    }
    public void AllRestore()
    {
        int count = factory.AllObj.Count;
        for (int i = 0; i < count; i++)
        {
            factory.AllObj[i].Restore();
        }
    }
}
[System.Serializable]
public class Factory
{
    int LAST_INDEX => pool.Count - 1;

    RecycleObject prefab;
    RecycleObject tempObject;
    public List<RecycleObject> AllObj => allObj;
    List<RecycleObject> pool = new List<RecycleObject>();
    [SerializeField] List<RecycleObject> allObj = new List<RecycleObject>();
    int defaultPoolSize;

    public Factory(RecycleObject prefab, int defaultPoolSize)
    {
        if (defaultPoolSize <= 0)
        {
            defaultPoolSize = 1;
        }

        this.prefab = prefab;
        this.defaultPoolSize = defaultPoolSize;
    }
    // public void Clear()
    // {
    //     for (int i = 0; i < allObj.Count; i++)
    //     {
    //         allObj[i].transform.SetParent(FactoryManager.Instance.transform);
    //     }
    // }

    public void CreatPool()
    {
        for (int i = 0; i < defaultPoolSize; i++)
        {
            tempObject = GameObject.Instantiate(prefab, new Vector3(-99999, -99999, 0), Quaternion.identity);
            tempObject.Initialize(Restore);
            tempObject.gameObject.SetActive(false);
            pool.Add(tempObject);
            allObj.Add(tempObject);
        }
    }

    public RecycleObject Get()
    {
        if (pool.Count <= 0)
        {
            CreatPool();
        }

        tempObject = pool[LAST_INDEX];
        pool.RemoveAt(LAST_INDEX);
        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void Restore(RecycleObject recycleObject)
    {
        recycleObject.gameObject.SetActive(false);
        pool.Add(recycleObject);
    }
}
