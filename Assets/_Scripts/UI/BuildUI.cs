using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
    [SerializeField] Button buildButton;
    public void Initialize()
    {
        buildButton.onClick.AddListener(TowerManager.Instance.RandomBuildTower);
    }
}
