using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBall : BonusBase
{
    protected new void Start()
    {
        base.Start();
    }

    public override void BonusActivate()
    {
        player_script.game_data.balls += 1;
    }
}
