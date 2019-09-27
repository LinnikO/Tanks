using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Tank
{
    [SerializeField] int killScore;
    [SerializeField] Tower tower;

    public override void Fire()
    {
        tower.Fire();
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
