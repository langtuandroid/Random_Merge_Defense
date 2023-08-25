using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameResourceManager : MonoBehaviour
{
    static ImageResources _imageResources;
    public static ImageResources ImageResources => _imageResources;
    public async Task Initialize()
    {
        _imageResources = new ImageResources();
        await _imageResources.Initialize();
    }
}
