using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    private bool active = false;

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

    public void Fire() {
        if (Active && CanFireNow())
        {
            LaunchProjectiles();
        }
    }

    protected abstract bool CanFireNow();

    protected abstract void LaunchProjectiles();
}
