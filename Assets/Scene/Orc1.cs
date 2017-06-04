using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{

    public Vector3 moveBy;
    Vector3 pointA;
    Vector3 pointB;
    Rigidbody2D myBody;
    public float speed;
    Vector3 rab;
    public Animator animator;

    bool attack = false;


    bool isHit = false;
    public float wait = 1;
    bool dead = false;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        pointA = transform.position;
        pointB = pointA + moveBy;
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

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }


        animator = GetComponent<Animator>(); // run-idle
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
        if (dead)
        {
            animator.SetBool("die", true);
        }



        rab = HeroRabbit.lastRabbit.transform.position;
        Vector3 my = transform.position;

        if (rab.x > Mathf.Min(pointA.x, pointB.x) && rab.x < Mathf.Max(pointA.x, pointB.x))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }


        if (isHit) wait -= Time.deltaTime;
        if (wait < 0)
        {
            Destroy(this.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            HeroRabbit r = coll.gameObject.GetComponent<HeroRabbit>();
            Vector3 my = this.transform.position;
            Vector3 it = r.transform.position;

            if (r != null && it.y > my.y)
            {
                this.dead = true;
                isHit = true;
            }
        }
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) <= 0.2f;
    }

    float dir = 1;
    bool toB = true;
    float getDirection()
    {
        Vector3 my = transform.position;
        if (attack)
        {
            if (my.x < rab.x)
            {
                return 1;
            }
            else return -1;
        }
        else
        {

            if (toB)
            {
                if (isArrived(my, pointB) == false) dir = 1;
                else
                {
                    dir = -1;
                    toB = false;
                }
            }
            if (!toB)
            {
                if (isArrived(my, pointA) == false) dir = -1;
                else
                {
                    dir = 1;
                    toB = true;
                }
            }
        }
        return dir;
    }
}
