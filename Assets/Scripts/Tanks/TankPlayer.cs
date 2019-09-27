using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer : Tank
{
    [SerializeField] List<Tower> towers;

    private int activeTowerIndex = 0;
    private Tower activeTower;
    private Vector2 targetPoint;
    private bool isAcite = false;

    public override int Armor {
        get { return base.Armor; }
        set {
            base.Armor = value;
            EventManager.TriggerEvent(EventType.PLAYER_ARMOR_CHANGED, armor);
        }
    }

    private void Start()
    {
        SpawnTank(Vector2.zero);
    }

    public override void Fire()
    {
        activeTower.Fire();
    }

    public override void SpawnTank(Vector2 position)
    {
        base.SpawnTank(position);
        this.gameObject.SetActive(true);
        ActivateNewTower();
    }

    public override void RotataTowerTo(Vector2 targetPoint)
    {
        this.targetPoint = targetPoint;
        activeTower.RotateToPoint(targetPoint);
    }

    public void NextWeapon() {
        activeTowerIndex++;
        if (activeTowerIndex == towers.Count) {
            activeTowerIndex = 0;
        }
        ActivateNewTower();
    }

    public void PreviousWeapon() {
        activeTowerIndex--;
        if (activeTowerIndex < 0)
        {
            activeTowerIndex = towers.Count - 1;
        }
        ActivateNewTower();
    }

    private void ActivateNewTower() {
        for (int i = 0; i < towers.Count; i++) {
            if (i == activeTowerIndex)
            {
                towers[i].Active = true;
                activeTower = towers[i];
                activeTower.RotateToPoint(targetPoint);
            }
            else
            {
                towers[i].Active = false;
            }
        }
    }

    protected override void DestroyTank()
    {
        tankMovement.CanMove = false;     
        this.gameObject.SetActive(false);
        EventManager.TriggerEvent(EventType.PLAYER_KILLED, null);
    }
}
