﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    bool going_to_a = false;

    public float Speed;
    public Vector3 mySpeed;
    public float WaitTime;

    public float time_to_wait;



    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        this.time_to_wait = this.WaitTime;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

    // Update is called once per frame
    void FixedUpdate () {
        time_to_wait -= Time.deltaTime;
        if (time_to_wait > 0)
        {
        }

        Vector3 my_pos = this.transform.position;
         Vector3 target;

         if (going_to_a)
         {
             target = this.pointA;
         }
         else
         {
             target = this.pointB;
         }
        

        if (isArrived(target, my_pos))
        {
            going_to_a = !going_to_a;
            time_to_wait = this.WaitTime;
            mySpeed = -mySpeed;
          }
        else
        {
            this.transform.Translate(mySpeed * Time.deltaTime);
          /*  Vector3 destination = target - my_pos;
            float move = this.Speed * Time.deltaTime;
            float distanse = Vector3.Distance(destination, my_pos);
            Vector3 move_vec = destination.normalized * Mathf.Min(move, distanse);
            this.transform.position = move_vec;*/
            // destination.z = 0;
        }}
       
     

    
 }

