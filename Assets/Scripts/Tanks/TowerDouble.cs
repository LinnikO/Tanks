using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDouble : Tower
{
    protected override bool CanFireNow()
    {
        return true;
    }

    protected override void LaunchProjectiles()
    {
        print("piw double");
    }
}
