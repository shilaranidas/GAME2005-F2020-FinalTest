﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    STATIC,
    DYNAMIC
}


[System.Serializable]
public class RigidBody3D : MonoBehaviour
{
    [Header("Gravity Simulation")]
    public float gravityScale;
    public float mass;
    public BodyType bodyType;
    public float timer;
    public bool isFalling;

    [Header("Attributes")]
    public Vector3 velocity;
    public Vector3 acceleration;
    public float gravity;
    public float restitution;
      public float friction;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        gravity = -0.001f;
        velocity = Vector3.zero;
        acceleration = new Vector3(0.0f, gravity * gravityScale, 0.0f);
       
        if (bodyType == BodyType.DYNAMIC)
        {
            isFalling = true;
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        if (bodyType == BodyType.DYNAMIC)
        {
            if (isFalling)
            {
                timer += Time.deltaTime;
                
                if (gravityScale < 0)
                {
                    gravityScale = 0;
                }

                if (gravityScale > 0)
                {
                  //  Debug.Log("rg "+ velocity+" "+velocity.magnitude);
                    velocity += acceleration * timer *timer;
                     
                    transform.position += velocity;
                }
            }
            else
            {
               //  timer += Time.deltaTime;
              //  velocity += acceleration * Time.deltaTime ;
                velocity.y = 0;
             //   transform.position += velocity* Time.deltaTime;
              //  Debug.Log("rg "+ velocity+" "+velocity.magnitude);
               // velocity += acceleration * 0.5f * timer ;
              //  transform.position += velocity* timer;
            }
        }
    }

    public void Stop()
    {
        timer = 0;
        isFalling = false;
    }
}
