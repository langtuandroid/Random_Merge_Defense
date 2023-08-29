using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;

    public void SetGoldText(int goldAmount)
    {
        Debug.Log(goldAmount);
        goldText.text = string.Format($"{goldAmount}");
    }
}
