using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCross : Tower
{
    [SerializeField] float cooldown;
    [SerializeField] Transform shootPointForward;
    [SerializeField] Transform shootPointLeft;
    [SerializeField] Transform shootPointRight;
    [SerializeField] Transform shootPointBack;

    private float shootTime = 0;

    protected override bool CanFireNow()
    {
        return Time.time - shootTime > cooldown;
    }

    protected override void Fire()
    {
        shootTime = Time.time;
        Vector2 forward = transform.right;
        Vector2 right = new Vector2(forward.y, -forward.x);
        Vector2 left = -right;
        Vector2 back = -forward;
        LaunchProjectile(shootPointForward.position, forward);
        LaunchProjectile(shootPointLeft.position, left);
        LaunchProjectile(shootPointRight.position, right);
        LaunchProjectile(shootPointBack.position, back);
    }
}
