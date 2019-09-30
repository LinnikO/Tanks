using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyLogic : EnemyLogicBase
{
    [SerializeField] float randomMoveTimeMin;
    [SerializeField] float randomMoveTimeMax;
    [SerializeField] float chasePlayerTime;

    private bool chasePlayer;
    private float randomMoveTime;
    private float startRandomTime;
    private float startChaseTime;

    public override void Init(Transform playerTransform)
    {
        base.Init(playerTransform);
        StartRandomMove();
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
                RandomMoveUpdate();
            }
        }
    }

    private void StartRandomMove() {
        chasePlayer = false;
        randomMoveTime = Random.Range(randomMoveTimeMin, randomMoveTimeMax);
        startRandomTime = Time.time;
        FindMovePath(GetRandomFieldPoint());
    }

    private void StartChasePlayer() {
        chasePlayer = true;
        startChaseTime = Time.time;
        FindMovePath(playerTransform.position);
    }

    private void CheckMoveState() {
        if (chasePlayer)
        {
            if (Time.time - startChaseTime > chasePlayerTime)
            {
                StartRandomMove();
            }
        }
        else
        {
            if (Time.time - startRandomTime > randomMoveTime)
            {
                StartChasePlayer();
            }
        }
    }

    private void RandomMoveUpdate() {
        if (movePath == null || movePath.Count == 0)
        {
            FindMovePath(GetRandomFieldPoint());
        }
    }
}
