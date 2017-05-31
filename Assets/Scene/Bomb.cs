using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	public override void OnRabbitEnter(HeroRabbit rab)
    {
        LevelInfo.current.onRabbitDeath(rab);
        this.hideCollectable();
    }
}
