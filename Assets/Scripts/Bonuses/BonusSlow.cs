using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSlow : BonusBase
{
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
            bs.ball_initial_force *= 0.9f;
        }
    }
}
