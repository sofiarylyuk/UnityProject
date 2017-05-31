using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
   
    public virtual void OnRabbitEnter(HeroRabbit rab){
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
            HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
            if (rabit != null)
            {
                this.OnRabbitEnter(rabit);
            }
    }
    public void hideCollectable()
    {
        Destroy(this.gameObject);
    }
}