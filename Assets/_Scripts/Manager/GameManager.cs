using System.Collections;
using System.Collections.Generic;
using PrimeScene;
using UnityEngine;
using SaveData;
using System.Threading.Tasks;

public enum GamePhase { Loading, Ingame, Lobby }
public class GameManager : SingletonComponent<GameManager>
{
    SceneManager sceneManager;
    public bool Loading => _loading;
    bool _loading = true;
    new private async void Awake()
    {
        // AllLayer.Initialize();

        Application.targetFrameRate = 60;
        //데이터 테이블,데이터 베이스 초기화
        await GetComponentInChildren<DataManager>().Initialize();
        //오브젝트풀링 FactoryManager 초기화
        await GetComponentInChildren<FactoryManager>().Initialize();
        //데이터 테이블,데이터 베이스 초기화
        await GetComponentInChildren<GameResourceManager>().Initialize();
        //스테이지메니저 초기화
        StageDataLayer stageDataLayer = DataManager.Database.StageDataLayer;

        if (stageDataLayer.GetData() == null)
        {
            StageData stageData = new StageData();
            stageData.currentStageId = 1;
            stageDataLayer.SetData(stageData);
        }

        sceneManager = new SceneManager();
        //만약 게임도중 나왔다면

        await Ingame(stageDataLayer.GetData().currentStageId);
        _loading = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
    }
    public async Task Ingame(int stageId)
    {
        FactoryManager.Instance.AllRestore();
        await sceneManager.LoadIdleScene();
        await sceneManager.LoadSelectedStageScene(stageId);
    }
    async Task Lobby()
    {
        await sceneManager.LoadLobbyScene();
    }


}
