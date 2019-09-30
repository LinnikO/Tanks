using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] GameObject projectileNormal;
    [SerializeField] GameObject projectileSmall;

    public Projectile GetProjectile(ProjectileType type) {
        switch (type)
        {
            case ProjectileType.NORMAL:
                GameObject projectileNormal = EasyObjectPool.instance.GetObjectFromPool("ProjectileNormal", Vector3.zero, Quaternion.identity);
                return projectileNormal.GetComponent<Projectile>();               
            case ProjectileType.SMALL:
                GameObject projectileSmall = EasyObjectPool.instance.GetObjectFromPool("ProjectileSmall", Vector3.zero, Quaternion.identity);
                return projectileSmall.GetComponent<Projectile>();
            default:
                return null;
        }
    }
}
