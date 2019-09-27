using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] TankMovement tankMovement;
    [SerializeField] TankPlayer tankPlayer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            tankMovement.Move(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            tankMovement.Move(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            tankMovement.Move(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            tankMovement.Move(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            tankPlayer.PreviousWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            tankPlayer.NextWeapon();
        }

        if (Input.GetMouseButton(0)) {
            tankPlayer.Fire();
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tankPlayer.RotataTowerTo(mousePoint);
        }
    }
}
