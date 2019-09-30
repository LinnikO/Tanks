using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Tank
{
    [SerializeField] int killScore;
    [SerializeField] Tower tower;

    private void Awake()
    {
        tower.Active = true;
    }

    public override void Fire()
    {
        tower.TryFire();
    }

    public override void RotataTowerTo(Vector2 targetPoint)
    {
        tower.RotateToPoint(targetPoint);
    }

    protected override void DestroyTank()
    {
        EventManager.TriggerEvent(EventType.ENEMY_DESTROYED, killScore);
        Destroy(this.gameObject);
    }
}
