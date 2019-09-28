using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] Image towerImage;
    [SerializeField] Text towerName;
    [SerializeField] Sprite simpleTower;
    [SerializeField] Sprite doubleTower;
    [SerializeField] Sprite crossTower;

    private void OnEnable()
    {
        EventManager.AddListener(EventType.PLAYER_TOWER_CHANGED,  OnTowerChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventType.PLAYER_TOWER_CHANGED, OnTowerChanged);
    }

    private void OnTowerChanged(object args) {
        TowerType type = (TowerType)args;
        switch (type)
        {
            case TowerType.SIMPLE:
                towerImage.sprite = simpleTower;
                towerImage.SetNativeSize();
                towerName.text = "SIMPLE TOWER";
                break;
            case TowerType.DOUBLE:
                towerImage.sprite = doubleTower;
                towerImage.SetNativeSize();
                towerName.text = "DOUBLE TOWER";
                break;
            case TowerType.CROSS:
                towerImage.sprite = crossTower;
                towerImage.SetNativeSize();
                towerName.text = "CROSS TOWER";
                break;          
        }
    }
}
