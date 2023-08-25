using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using SaveData;
public class DataManager : MonoBehaviour
{
    public static DataBase Database;
    public static DataTableBase DataTableBase;
    public Task Initialize()
    {
        Database = new DataBase();
        DataTableBase = new DataTableBase();
        if (SaveGame.Exists("Data"))
        {
            Database = SaveGame.Load("Data", Database);
        }
        else
        {
            Database.Initialize();
        }
        return DataTableBase.Init();
    }
    // void OnApplicationPause(bool pauseStatus)
    // {
    //     if (pauseStatus && !GameManager.Instance.Loading)
    //     {
    //         Save();
    //     }
    // }
    // void OnApplicationQuit()
    // {
    //     if (!GameManager.Instance.Loading)
    //         Save();
    // }
    // void OnApplicationFocus(bool focusStatus)
    // {
    //     if (!focusStatus && !GameManager.Instance.Loading)
    //     {
    //         Save();
    //     }
    // }

}