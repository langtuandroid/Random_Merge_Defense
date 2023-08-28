using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

[System.Serializable]
public class InGameDataLayer : DataLayer
{
    [SerializeField] InGameData inGameData;
    public InGameData GetData()
    {
        return inGameData;
    }
    public override void SetData<T>(T value)
    {
        inGameData = value as InGameData;
    }
}
