using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
namespace PrimeScene
{
    public class SceneManager
    {
        string _lobbySceneId;
        string _stageSceneId = "Stage";
        string _idleSceneId = "Idle";
        public Task LoadLobbyScene()
        {
            return LoadScene(_lobbySceneId);
        }
        public Task LoadSelectedStageScene(int stageId)
        {
            return LoadScene(_stageSceneId + stageId.ToString());
        }
        public Task LoadIdleScene()
        {
            return LoadScene(_idleSceneId);
        }


        Task LoadScene(string sceneName)
        {
            var handle = Addressables.LoadSceneAsync(sceneName);
            return handle.Task;
        }
    }
}
