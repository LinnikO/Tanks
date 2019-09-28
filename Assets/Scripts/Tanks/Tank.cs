using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tank : MonoBehaviour
{
    [SerializeField]
    private int fullArmor;
    [SerializeField]
    protected TankMovement tankMovement;
    [SerializeField]
    private Slider armorSlider;

    protected int armor;

    public virtual int Armor {
        get { return armor; }
        set {
            armor = value;
            armorSlider.value = (float)armor / fullArmor;
            if (armor <= 0) {
                DestroyTank();
            }
        }
    }

    public virtual void SpawnTank(Vector2 position)
    {
        transform.position = position;
        tankMovement.CanMove = true;
        Armor = fullArmor;
    }

    public void TakeDamage(int damage) {
        Armor -= damage;
    }

    public abstract void RotataTowerTo(Vector2 targetPoint);

    public abstract void Fire();

    protected abstract void DestroyTank();
    
}

