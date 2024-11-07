using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlus2 : BonusBase
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    public override void BonusActivate()
    {
        
        player_script.game_data.PlusBalls(2);
        player_script.game_data.balls += 2;
        player_script.CreateBalls(2);
    }
}
