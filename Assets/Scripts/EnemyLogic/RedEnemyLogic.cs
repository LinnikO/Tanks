using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyLogic : EnemyLogicBase
{
    [SerializeField] float minDisatanceToPlayer;
    [SerializeField] float escapeDisatance;
    [SerializeField] float escapeTime;

    private bool chasePlayer;
    private float startEscapeTime;

    public override void Init(Transform playerTransform)
    {
        base.Init(playerTransform);
        StartChase();
    }

    protected override void Update()
    {
        base.Update();
        if (initialized)
        {
            CheckMoveState();
            if (chasePlayer)
            {
                ChasePlayerUpdate();
            }
            else
            {
                EscapeUpdate();
            }
        }
    }

    private void StartChase() {
        chasePlayer = true;
        FindMovePath(playerTransform.position);
    }

    private void StartEscape() {
        chasePlayer = false;
        startEscapeTime = Time.time;
        FindMovePath(GetEscapePoint());
    }

    private void EscapeUpdate()
    {
        if (movePath == null || movePath.Count == 0)
        {
            FindMovePath(GetEscapePoint());
        }
    }

    private void CheckMoveState()
    {
        if (chasePlayer)
        {
            if ((transform.position - playerTransform.position).magnitude < minDisatanceToPlayer) {
                StartEscape();
            }
        }
        else
        {
            if (Time.time - startEscapeTime > escapeTime)
            {
                StartChase();
            }
        }
    }

    private Vector2 GetEscapePoint() {
        Vector2 opositDirection = -((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        Vector2 escapePoint = (Vector2)transform.position + opositDirection * escapeDisatance;

        //If escape poit out of field, move point to other side of field
        if (escapePoint.x < gameFieldSize.xMin)
        {
            escapePoint.x = escapePoint.x + gameFieldSize.width;
        }
        else if (escapePoint.x > gameFieldSize.xMax) {
            escapePoint.x = escapePoint.x - gameFieldSize.width;
        }
        if (escapePoint.y < gameFieldSize.yMin)
        {
            escapePoint.y = escapePoint.y + gameFieldSize.width;
        }
        else if (escapePoint.y > gameFieldSize.yMax)
        {
            escapePoint.y = escapePoint.y - gameFieldSize.width;
        }

        return escapePoint;
    }
}
