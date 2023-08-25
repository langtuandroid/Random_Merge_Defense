using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;
[System.Serializable]
public class PlayerDataLayer : DataLayer
{
    // [SerializeField] SaveData.PlayerData _playerData;
    // public SaveData.PlayerData GetData()
    // {
    //     return null;
    //     // return _playerData;
    // }
    public override void SetData<T>(T value)
    {
        // _playerData = value as SaveData.PlayerData;
    }
}
