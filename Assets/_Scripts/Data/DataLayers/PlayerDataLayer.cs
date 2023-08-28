using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;
[System.Serializable]
public class PlayerDataLayer : DataLayer
{
    [SerializeField] PlayerData _playerData;
    public PlayerData GetData()
    {
        return _playerData;
    }
    public override void SetData<T>(T value)
    {
        _playerData = value as PlayerData;
    }
}
