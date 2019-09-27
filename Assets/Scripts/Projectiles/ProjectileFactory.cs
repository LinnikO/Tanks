using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] GameObject projectileNormal;
    [SerializeField] GameObject projectileSmall;

    public Projectile GetProjectile(ProjectileType type) {
        switch (type)
        {
            case ProjectileType.NORMAL:
                return Instantiate(projectileNormal).GetComponent<Projectile>();               
            case ProjectileType.SMALL:
                return Instantiate(projectileSmall).GetComponent<Projectile>();
            default:
                return null;
        }
    }
}
