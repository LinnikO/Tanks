using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksFactory : MonoBehaviour
{
    [SerializeField] GameObject playerTank;
    [SerializeField] GameObject enemyGreenTank;
    [SerializeField] GameObject enemyYellowTank;
    [SerializeField] GameObject enemyRedTank;

    public Tank GetTank(TankType type) {
        switch (type)
        {
            case TankType.PLAYER:
                return Instantiate(playerTank).GetComponent<Tank>();
            case TankType.ENEMY_GREEN:
                return Instantiate(enemyGreenTank).GetComponent<Tank>();
            case TankType.ENEMY_YELLOW:
                return Instantiate(enemyYellowTank).GetComponent<Tank>();
            case TankType.ENEMY_RED:
                return Instantiate(enemyRedTank).GetComponent<Tank>();
            default:
                return null;
        }
    }
}

public enum TankType {
    PLAYER,
    ENEMY_GREEN,
    ENEMY_YELLOW,
    ENEMY_RED
}
