using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFast : BonusBase
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        ballScripts = FindObjectsOfType<BallScript>();
    }

    public override void BonusActivate()
    {
        foreach (var bs in ballScripts)
        {
            if (bs == null) continue;
            bs.ball_initial_force *= 1.1f;
        }
    }
}
