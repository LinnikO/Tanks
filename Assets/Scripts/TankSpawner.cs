using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] List<Vector2> enemySpawnPoints;
    [SerializeField] Vector2 playerSpawnPoint;
    [SerializeField] float spawnCooldown;
    [SerializeField] int maxEnemies;
    [SerializeField] TanksFactory tanksFactory;

    private SpawnPhase phase;
    private List<TankType> enemiesTypes;
    private Tank tankPlayer;
    private Vector2 lastSpawnPoint;
    private float lastSpawnTime;
    private int enemiesSpawned;
    private int enemiesDestroyed;

    private void Awake()
    {
        enemiesTypes = new List<TankType> { TankType.ENEMY_GREEN, TankType.ENEMY_YELLOW, TankType.ENEMY_RED };
        lastSpawnTime = Time.time;
        enemiesSpawned = 0;
        enemiesDestroyed = 0;
        StartSpawn();
    }

    private void OnEnable()
    {
        EventManager.AddListener(EventType.ENEMY_DESTROYED, OnEnemyDestroyed);
        EventManager.AddListener(EventType.RESPAWN_PLAYER, OnRespawnPlayer);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventType.ENEMY_DESTROYED, OnEnemyDestroyed);
        EventManager.RemoveListener(EventType.RESPAWN_PLAYER, OnRespawnPlayer);
    }

    private void StartSpawn() {
        SpawnPlayer();
        phase = SpawnPhase.FIST;
    }

    private void Update()
    {
        SpawnUpdate();
    }

    private void SpawnUpdate() {
        if (Time.time - lastSpawnTime < spawnCooldown)
        {
            return;
        }

        switch (phase)
        {
            case SpawnPhase.FIST:
                FirsPhaseUpdate();
                break;
            case SpawnPhase.SECOND:
                SecondPhaseUpdate();
                break;
            case SpawnPhase.THIRD:
                ThirdPhaseUpdate();
                break;
        }
    }

    private void FirsPhaseUpdate() {
        if (enemiesSpawned < 5)
        {
            SpawnEnemy(TankType.ENEMY_GREEN);
        }
    }

    private void SecondPhaseUpdate()
    {
        if (enemiesSpawned < 10)
        {
            SpawnEnemy(TankType.ENEMY_YELLOW);
        }
    }

    private void ThirdPhaseUpdate()
    {
        if (enemiesSpawned - enemiesDestroyed < maxEnemies)
        {
            TankType type = enemiesTypes[Random.Range(0, enemiesTypes.Count)];
            SpawnEnemy(type);
        }
    }

    private void SpawnPlayer() {
        if (tankPlayer == null) {
            tankPlayer = tanksFactory.GetTank(TankType.PLAYER);
        }
        tankPlayer.SpawnTank(playerSpawnPoint);
    }

    private void SpawnEnemy(TankType type)
    {
        Vector2 spawnPoint = GetEnemySpawnPoint();
        Tank tank = tanksFactory.GetTank(type);
        tank.SpawnTank(spawnPoint);
        enemiesSpawned++;
        lastSpawnPoint = spawnPoint;
        lastSpawnTime = Time.time;
        tank.GetComponent<EnemyLogicBase>().Init(tankPlayer.transform);
    }

    private Vector2 GetEnemySpawnPoint() {
        Vector2 spawnPoint;
        do
        {
            spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
        } while (spawnPoint == lastSpawnPoint);
        return spawnPoint;
    }

    private void CheckPahse() {
        if (phase == SpawnPhase.FIST && enemiesDestroyed >= 3) {
            phase = SpawnPhase.SECOND;
        }
        else if (phase == SpawnPhase.SECOND 
            && enemiesSpawned == 10
            && enemiesDestroyed >= 7)
        {
            phase = SpawnPhase.THIRD;
        }
    }

    private void OnEnemyDestroyed(object args) {
        enemiesDestroyed++;
        CheckPahse();
    }

    private void OnRespawnPlayer(object args) {
        SpawnPlayer();
    }
}

public enum SpawnPhase
{
    FIST,
    SECOND,
    THIRD
}
