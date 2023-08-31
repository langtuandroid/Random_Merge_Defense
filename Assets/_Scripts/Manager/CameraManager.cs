using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UniRx;
using UniRx.Triggers;
using System;

public class CameraManager : SingletonComponent<CameraManager>
{
    public void Initialie()
    {
        Camera camera = GetComponent<Camera>();
        float orthographicSize = camera.orthographicSize;
        float value = ((Screen.height / (Screen.width / 1080f)) / 1920f);
        if (value > 1)
            camera.orthographicSize = orthographicSize * value;
    }
}
