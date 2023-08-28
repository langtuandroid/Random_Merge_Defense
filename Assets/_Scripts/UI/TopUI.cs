using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifeText;

    public void Initialize()
    {
        SetLifeText(LifeManager.Instance.LifeAmount);
    }
    public void SetLifeText(int lifeAmount)
    {
        lifeText.text = string.Format($"{lifeAmount}");
    }
}
