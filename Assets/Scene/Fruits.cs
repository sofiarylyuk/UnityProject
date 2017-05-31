using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable {

    public override void OnRabbitEnter(HeroRabbit rab)
    {
        LevelInfo.current.addFruits(1);
        this.hideCollectable();
    }
}
