using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : Collectable {

    bool touched = false;

    public override void OnRabbitEnter(HeroRabbit rab)
    {
        rab.addHealth(1);
        rab.onHealthChange();
        this.hideCollectable();
    }
}
