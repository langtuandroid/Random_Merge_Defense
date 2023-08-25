using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private SlicedFilledImage gaugeBar;
    public float HpPercent => gaugeBar.fillAmount;


    public void Initialize()
    {
        transform.rotation = CameraManager.Instance.transform.rotation;
    }

    public void UpdateHPBar(float value)
    {
        gaugeBar.fillAmount = value;
    }

}
