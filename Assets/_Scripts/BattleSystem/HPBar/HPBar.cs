using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private SlicedFilledImage gaugeBar;
    public float HpPercent => gaugeBar.fillAmount;
    Quaternion fixedRotation;

    public void Initialize()
    {
        fixedRotation = CameraManager.Instance.transform.rotation;
        transform.rotation = fixedRotation;
    }
    private void Update()
    {

        transform.rotation = fixedRotation;
    }

    public void UpdateHPBar(float value)
    {
        gaugeBar.fillAmount = value;
    }

}
