using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSimple : Tower
{
    protected override bool CanFireNow()
    {
        return true;
    }

    protected override void LaunchProjectiles()
    {
        print("piw simple");
    }
}
