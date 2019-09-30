using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyLogic : EnemyLogicBase
{
    protected override void Update()
    {
        base.Update();
        if (initialized)
        {
            ChasePlayerUpdate();
        }
    }
}
