using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarrot : Collectable
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(destroyLater());
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }



    public void launch(float dir)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
