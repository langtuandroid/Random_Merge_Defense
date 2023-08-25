using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;
[System.Serializable]
public class StageDataLayer : DataLayer
{

    [SerializeField] StageData _stageData;
    public StageDataLayer()
    {
        // _stageData = new StageData();
    }
    public StageData GetData()
    {
        return _stageData;
    }
    public override void SetData<T>(T value)
    {
        _stageData = value as StageData;
    }
}
