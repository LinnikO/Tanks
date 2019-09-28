using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSimple : Tower
{
    [SerializeField] Transform shootPoint;

    private bool canFire = true;

    protected override bool CanFireNow()
    {
        return canFire;
    }

    protected override void Fire()
    {
        canFire = false;
        Projectile projectile = LaunchProjectile(shootPoint.position, transform.right);
        projectile.Destroyed += ProjectileDestroyed;
    }

    private void ProjectileDestroyed() {
        canFire = true;
    }
}
