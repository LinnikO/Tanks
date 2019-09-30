using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogicBase : MonoBehaviour
{
    public struct PathPoint {
        public Vector2 point;
        public Vector2 direction;
    }

    [SerializeField] protected Rect gameFieldSize;
    [SerializeField] TankMovement tankMovement;
    [SerializeField] Tank tank;
    [SerializeField] float shootDisatance;

    protected List<PathPoint> movePath;
    protected int pathIndex;
    protected Vector2 moveDirection;
    protected Transform playerTransform;
    protected bool initialized;
    private int playerLayerMask;

    public virtual void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        initialized = true;
    }

    protected virtual void Update()
    {
        if (initialized)
        {
            MoveUpdate();
            FireUpdate();
        }
    }

    private void MoveUpdate() {
        if (movePath == null || movePath.Count == 0) {
            return;
        }

        PathPoint pathPoint = movePath[pathIndex];
        float maxDistance = (pathPoint.point - (Vector2)transform.position).magnitude;
        tankMovement.Move(pathPoint.direction, maxDistance);
        tank.RotataTowerTo((Vector2)transform.position + pathPoint.direction);
        moveDirection = pathPoint.direction;
        if ((Vector2)transform.position == pathPoint.point)
        {
            pathIndex++;
            if (pathIndex >= movePath.Count) {
                movePath.Clear();
                pathIndex = 0;
            }
        }
    }

    protected void FindMovePath(Vector2 target)
    {
        if (movePath == null) {
            movePath = new List<PathPoint>();
        }
        movePath.Clear();
        pathIndex = 0;

        float distanceX;
        float distanceY;
        bool reversX;
        bool reversY;

        GetDisatanceX(out distanceX, out reversX, (Vector2)transform.position, target);
        GetDisatanceY(out distanceY, out reversY, (Vector2)transform.position, target);
        
        PathPoint pathPoint1 = new PathPoint();
        PathPoint pathPoint2 = new PathPoint();

        pathPoint2.point = target;

        if (distanceX < distanceY)
        {
            pathPoint1.point = new Vector2(target.x, transform.position.y);
        }
        else {
            pathPoint1.point = new Vector2(transform.position.x, target.y);
        }

        pathPoint1.direction = (pathPoint1.point - (Vector2)transform.position).normalized;
        pathPoint2.direction = (pathPoint2.point - pathPoint1.point).normalized;

        if (distanceX < distanceY)
        {
            if (reversX)
            {
                pathPoint1.direction = -pathPoint1.direction;
            }
            if (reversY)
            {
                pathPoint2.direction = -pathPoint2.direction;
            }

        }
        else {
            if (reversX)
            {
                pathPoint2.direction = -pathPoint2.direction;
            }
            if (reversY)
            {
                pathPoint1.direction = -pathPoint1.direction;
            }
        }

        movePath.Add(pathPoint1);
        movePath.Add(pathPoint2);
    }

    private void GetDisatanceX(out float distance, out bool revers, Vector2 point1, Vector2 point2) {
        float forwardDisatance = point2.x - point1.x;
        float reversDisatance = GetReversDisatanceX(point1, point2);

        if (forwardDisatance < reversDisatance)
        {
            distance = Mathf.Abs(forwardDisatance);
            revers = false;
        }
        else {
            distance = Mathf.Abs(reversDisatance);
            revers = true;
        }
    }

    private void GetDisatanceY(out float distance, out bool revers, Vector2 point1, Vector2 point2)
    {
        float forwardDisatance = point2.y - point1.y;
        float reversDisatance = GetReversDisatanceY(point1, point2);

        if (forwardDisatance < reversDisatance)
        {
            distance = Mathf.Abs(forwardDisatance);
            revers = false;
        }
        else
        {
            distance = Mathf.Abs(reversDisatance);
            revers = true;
        }
    }

    private float GetReversDisatanceX(Vector2 point1, Vector2 point2) {
        Vector2 leftPoint;
        Vector2 rightPoint;
        if (point1.x < point2.x)
        {
            leftPoint = point1;
            rightPoint = point2;
        }
        else {
            leftPoint = point2;
            rightPoint = point1;
        }

        float reversDisatance = leftPoint.x - gameFieldSize.xMin + gameFieldSize.xMax - rightPoint.x;
        return reversDisatance;
    }

    private float GetReversDisatanceY(Vector2 point1, Vector2 point2)
    {
        Vector2 topPoint;
        Vector2 bottomPoint;
        if (point1.y > point2.y)
        {
            topPoint = point1;
            bottomPoint = point2;
        }
        else
        {
            topPoint = point2;
            bottomPoint = point1;
        }

        float reversDisatance = gameFieldSize.yMax - topPoint.y + bottomPoint.y - gameFieldSize.yMin;
        return reversDisatance;
    }

    private void FireUpdate() {
        if (IsPlayerInFront()) {           
            tank.Fire();
        }
    }

    private bool IsPlayerInFront() {       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, shootDisatance, playerLayerMask);
        return hit.transform != null;
    }

    protected void ChasePlayerUpdate()
    {
        if (movePath == null || movePath.Count == 0)
        {
            FindMovePath(playerTransform.position);
        }
    }

    protected Vector2 GetRandomFieldPoint()
    {
        float x = Random.Range(gameFieldSize.xMin, gameFieldSize.xMax);
        float y = Random.Range(gameFieldSize.yMin, gameFieldSize.yMin);
        return new Vector2(x, y);
    }
}