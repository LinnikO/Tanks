using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public void LaunchProjectile(ProjectileOwner owner, Vector3 direction, int damage, float speed) {
        Owner = owner;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        isMoving = true;
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
            OnDestroyed();
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "Border")
        {
            OnDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnDestroyed()
    {
        if (Destroyed != null) {
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
