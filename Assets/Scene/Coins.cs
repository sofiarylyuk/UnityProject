using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable
{

    public override void OnRabbitEnter(HeroRabbit rab)
    {
        LevelInfo.current.addCoins(1);
        this.hideCollectable();
    }
}
