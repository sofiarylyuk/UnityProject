using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : MonoBehaviour
{
    public GameObject prCarrot;


    public Vector3 moveBy;
    Vector3 pointA;
    Vector3 pointB;
    Rigidbody2D myBody;
    public float speed;
    Vector3 rab;
    SpriteRenderer sr;

    bool attack = false;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        pointA = transform.position;
        pointB = pointA - moveBy;
    }

    void FixedUpdate()
    {
        //this.transform.Translate(speedVect * Time.deltaTime);
        //[-1, 1]
        float value = getDirection(); //velocity

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }

        Animator animator = GetComponent<Animator>(); // run-idle
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        rab = HeroRabbit.lastRabbit.transform.position;
        if (rab.x > Mathf.Min(pointA.x, pointB.x) && rab.x < Mathf.Max(pointA.x, pointB.x))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

        if (attack)
        {
            animator.SetBool("attack", true);

        }
        else
        {
            animator.SetBool("attack", false);
        }

    }

    void launchCarrot(float dir)
    {
        GameObject ob = GameObject.Instantiate(this.prCarrot);
        ob.transform.position = this.transform.position;
        WeaponCarrot c = ob.GetComponent<WeaponCarrot>();
        c.launch(dir);
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) <= 0.2f;
    }

    float dir = -1;
    bool toA = true;
    float getDirection()
    {
        Vector3 my = transform.position;
        if (attack)
        {
            if (my.x < rab.x)
            {
                sr.flipX = true;
                this.launchCarrot(1);
                return 0;
            }
            else
            {
                this.launchCarrot(-1);
                return 0;
            }
        }
        else
        {

            if (toA)
            {
                if (isArrived(my, pointB) == false) dir = -1;
                else
                {
                    dir = 1;
                    toA = false;
                }
            }
            if (!toA)
            {
                if (isArrived(my, pointA) == false) dir = 1;
                else
                {
                    dir = -1;
                    toA = true;
                }
            }
        }
        return dir;
    }
}
