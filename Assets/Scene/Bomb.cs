using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

     public float time_to_wait;
     bool touched = false;
     HeroRabbit rabbit;

	public override void OnRabbitEnter(HeroRabbit rab)
    {

        Debug.Log("before " + rab.health);
     if (rab.health == 2)
         {
            
            rab.removeHealth(1);
            Debug.Log("not dead?" + rab.health);
            rab.onHealthChange();
            this.hideCollectable();
         }

        else if (rab.health < rab.MaxHealth)
        {
            Debug.Log("the hell " + rab.health);
            rabbit = rab;
             touched = true;
             rab.dead = true;
        }

        
          
    }

    void FixedUpdate()
    {
        if (touched) time_to_wait -= Time.deltaTime;
        if (time_to_wait < 0)
        {
            rabbit.dead = false;
            LevelInfo.current.onRabbitDeath(rabbit);
            this.hideCollectable();
        }
    }
}

