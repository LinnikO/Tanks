using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rect gameFieldSize;
    [SerializeField] Transform tankBody;

    public bool CanMove { get; set; }

    public void Move(Vector2 direction)
    {
        if (!CanMove)
        {
            return;
        }

        Vector2 normalizedDirection = direction.normalized;
        transform.position = (Vector2)transform.position + normalizedDirection * moveSpeed * Time.deltaTime;
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tankBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        CheckBorders();
    }

    private void CheckBorders()
    {
        if (transform.position.x < gameFieldSize.xMin)
        {
            TeleportToRight();
        }
        else if (transform.position.x > gameFieldSize.xMax)
        {
            TeleportToLeft();
        }
        else if (transform.position.y > gameFieldSize.yMax)
        {
            TeleportToBottom();
        }
        else if (transform.position.y < gameFieldSize.yMin)
        {
            TeleportToTop();
        }
    }

    private void TeleportToLeft()
    {
        float x = gameFieldSize.xMin;
        transform.position = new Vector2(x, transform.position.y);
    }

    private void TeleportToRight()
    {
        float x = gameFieldSize.xMax;
        transform.position = new Vector2(x, transform.position.y);
    }

    private void TeleportToTop()
    {
        float y = gameFieldSize.yMax;
        transform.position = new Vector2(transform.position.x, y);
    }

    private void TeleportToBottom()
    {
        float y = gameFieldSize.yMin;
        transform.position = new Vector2(transform.position.x, y);
    }
}
