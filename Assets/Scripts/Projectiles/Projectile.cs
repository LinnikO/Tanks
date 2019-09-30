using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MarchingBytes;

public class Projectile : MonoBehaviour
{
    public event Action Destroyed;

    [SerializeField] Sprite playerProjectile;
    [SerializeField] Sprite enemyProjectile;
    [SerializeField] SpriteRenderer sprRenderer;

    private ProjectileOwner owner;
    private Vector2 direction;
    private int damage;
    private float speed;
    private bool isMoving = false;

    public ProjectileOwner Owner {
        get { return owner; }
        set {
            owner = value;
            if (owner == ProjectileOwner.PLAYER)
            {
                sprRenderer.sprite = playerProjectile;
            }
            else
            {
                sprRenderer.sprite = enemyProjectile;
            }
        }
    }

    public void LaunchProjectile(ProjectileOwner owner, Vector2 direction, int damage, float speed) {
        Owner = owner;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        isMoving = true;
        transform.right = direction;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving) {
            return;
        }

        if ((Owner == ProjectileOwner.PLAYER && collision.transform.tag == "Enemy")
            || (Owner == ProjectileOwner.ENEMY && collision.transform.tag == "Player"))
        {
            Tank tank = collision.gameObject.GetComponent<Tank>();
            tank.TakeDamage(damage);
            DestroyProjectile();
        }
        else if (collision.transform.tag == "Border")
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile() {
        isMoving = false;
        EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
        if (Destroyed != null)
        {
            Destroyed();
        }
    }
}

public enum ProjectileOwner {
    PLAYER,
    ENEMY
}

public enum ProjectileType {
    NORMAL,
    SMALL
}
