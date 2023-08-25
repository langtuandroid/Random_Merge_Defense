using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
public class ImageResources
{
    public struct ImageResourcesInfo
    {
        public string imageID;
        public Sprite sprite;

        public ImageResourcesInfo(string imageID, Sprite sprite)
        {
            this.imageID = imageID;
            this.sprite = sprite;
        }
    }

    private ImageResourcesInfo[] _imageResourcesInfos;
    public async Task Initialize()
    {
        AsyncOperationHandle<IList<IResourceLocation>> LoadResourcehandle = Addressables.LoadResourceLocationsAsync("Image", typeof(Sprite));
        await LoadResourcehandle.Task;
        int count = LoadResourcehandle.Result.Count;
        _imageResourcesInfos = new ImageResourcesInfo[count];
        int imageAmount = 0;
        foreach (var t in LoadResourcehandle.Result)
        {
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(t.PrimaryKey);
            await handle.Task;
            _imageResourcesInfos[imageAmount] = new ImageResourcesInfo(t.PrimaryKey, handle.Result);
            imageAmount++;
        }
        Addressables.Release(LoadResourcehandle);
    }
    public Sprite GetNormalImage(string imageId)
    {
        for (int i = 0; i < _imageResourcesInfos.Length; i++)
        {
            if (_imageResourcesInfos[i].imageID == imageId)
            {
                return _imageResourcesInfos[i].sprite;
            }
        }
        return null;
    }
}
