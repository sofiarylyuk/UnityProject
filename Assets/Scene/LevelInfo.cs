using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo current = null;

    void Awake()
    {
        current = this;
    }

    Vector3 strartingPoint;

    public void setRabbitsStartingPoint(Vector3 pos)
    {
        strartingPoint = pos;
    }

    public void onRabbitDeath(HeroRabbit rab)
    {
        rab.transform.position = this.strartingPoint;
    }

    public void addCoins(int numb)
    {
        Debug.Log("coins colected" + numb);
    }
}
