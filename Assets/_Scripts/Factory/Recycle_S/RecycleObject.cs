using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{

    public string id;
    public int poolSize = 5;
    System.Action<RecycleObject> restore;
    public virtual void Initialize(System.Action<RecycleObject> restore)
    {
        DontDestroyOnLoad(this);
        this.restore = restore;
    }

    public virtual void Restore()
    {
        if (gameObject.activeSelf)
            restore(this);
    }
}
