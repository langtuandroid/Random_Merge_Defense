using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WaveUI : MonoBehaviour
{
    [SerializeField] Button waveStartButton;
    [SerializeField] TextMeshProUGUI waveIntervalText;
    public void Initialize()
    {
        waveStartButton.onClick.AddListener(() =>
        {
            WaveManager.Instance.WaveStartImmediately();
        });
    }

    public void SetWaveIntervalText(float time)
    {
        waveIntervalText.text = string.Format("남은시간 : {0:0.00}", time);
    }
    public void On()
    {
        gameObject.SetActive(true);
    }
    public void Off()
    {
        gameObject.SetActive(false);
    }
}
