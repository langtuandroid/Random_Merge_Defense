using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] TowerData towerData;
    public void Initialize(TowerData towerData)
    {
        this.towerData = towerData;
    }
}
