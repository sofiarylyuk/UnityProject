using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : Collectable {

    public override void OnRabbitEnter(HeroRabbit rab)
    {
        LevelInfo.current.addCrystals(1);
        this.hideCollectable();
    }
}
