using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDouble : Tower
{
    [SerializeField] float cooldown;
    [SerializeField] Transform shootPoint1;
    [SerializeField] Transform shootPoint2;

    private float shootTime = 0;

    protected override bool CanFireNow()
    {
        return Time.time - shootTime > cooldown;
    }

    protected override void Fire()
    {       
        shootTime = Time.time;
        LaunchProjectile(shootPoint1.position, transform.right);
        LaunchProjectile(shootPoint2.position, transform.right);
    }
}
