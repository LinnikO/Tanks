using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float projectilesSpeed;
    [SerializeField]
    protected ProjectileOwner owner;
    [SerializeField]
    protected ProjectileType projectileType;
    [SerializeField]
    private ProjectileFactory projectileFactory;
    [SerializeField]
    private TowerType type;

    private bool active = false;

    public TowerType Type {
        get { return type; }
    }
    
    public bool Active {
        get { return active; }
        set {
            active = value;
            gameObject.SetActive(active);
        }
    }

    public void RotateToPoint(Vector2 targetPoint)
    {
        if (Active)
        {
            Vector2 dir = targetPoint - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void TryFire() {
        if (Active && CanFireNow())
        {
            Fire();
        }
    }

    protected abstract bool CanFireNow();

    protected abstract void Fire();

    protected Projectile LaunchProjectile(Vector2 startPoint, Vector2 direction)
    {
        Projectile projectile = projectileFactory.GetProjectile(projectileType);
        projectile.transform.position = startPoint;
        projectile.LaunchProjectile(owner, direction, damage, projectilesSpeed);
        return projectile;
    }
}
