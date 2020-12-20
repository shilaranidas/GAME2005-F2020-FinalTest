using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] cubes;
    public BulletBehaviour[] spheres;

    private static Vector3[] faces;

    // Start is called before the first frame update
    void Start()
    {
        cubes = FindObjectsOfType<CubeBehaviour>();

        faces = new Vector3[]
        {
            Vector3.left, Vector3.right,
            Vector3.down, Vector3.up,
            Vector3.back , Vector3.forward
        };
    }

    // Update is called once per frame
    void Update()
    {
        spheres = FindObjectsOfType<BulletBehaviour>();
        //for dynamic bullet include
       // cubes = FindObjectsOfType<CubeBehaviour>();
        // check each AABB with every other AABB in the scene
        for (int i = 0; i < cubes.Length; i++)
        {
            for (int j = 0; j < cubes.Length; j++)
            {
                if (i != j)
                {
                    CheckAABBs(cubes[i], cubes[j]);
                }
            }
        }
        //bool r = false;
        // Check each sphere against each AABB in the scene
        //foreach (var s in spheres)
        for (int k = 0; k < spheres.Length; k++)
        {
            var s = spheres[k];
            //foreach (var b in cubes)
            for (int i = 0; i < cubes.Length; i++)
            {
                var b = cubes[i];
                if (b.name != "Player")
                {
                    CheckAABBCube(s,b);
                    //r = false;
                   // CheckSphereAABB(s, b);//, out r);
                    //calculate Vr=Vb-Va
                    // Vector3 Vr = b.rb.velocity - s.rb.velocity;
                   // if (r)
                  //  {

                        // float v1 = Mathf.Abs(Vector3.Dot(s.vel, s.collisionNormal));
                        //  float v2 = Mathf.Abs(Vector3.Dot(b.rb.velocity, s.collisionNormal));

                        ////  float prop1 = v1 / (v1 + v2);
                        //   float prop2 = v2 / (v1 + v2);
                        //  if ((v1 + v2) == 0.0f)
                        //      prop1 = prop2 = 0.0f;

                        //  s.transform.position = s.transform.position + s.collisionNormal *  prop1;

                        //   if (!b.isGrounded)
                        //     b.transform.position = b.transform.position - s.collisionNormal *  prop2;


                        // Vector3 Vr = b.rb.velocity - s.vel;

                        // // Debug.Log("rel vel " + Vr);
                        // //nr=vr.n;
                        // //If this magnitude is greater than zero, the objects are moving away from each other and we can't apply any impulse.
                        // float Nr = Vector3.Dot(Vr, s.collisionNormal);
                        // Debug.Log("NR" + Nr);
                        // if (Nr < 0.0f)
                        // {
                        //     //impulse
                        //     //coefficient of restituion e=min(ea,eb)
                        //     float e = Mathf.Min(b.rb.restitution, s.restitution);
                        //     //impulse j=-(1+e)(vr.n)/((1/ma)+(1/mb))
                        //     float j = -(1 + e) * Nr / ((1 / b.rb.mass) + (1 / s.mass));
                        //     //tangent vector,t=vr-(vr.n)n=vr-Nr*n
                        //     Vector3 t = Vr - Nr * s.collisionNormal;
                        //     //magniture of impulse, jt=-(1+e)(vr.t)/((1/ma)+(1/mb))
                        //     float jt = -(1 + e) * Vector3.Dot(Vr, t) / ((1 / b.rb.mass) + (1 / s.mass));
                        //     //friction=sqrt(frictionA,frictionB)
                        //     float friction = Mathf.Sqrt(b.rb.friction * s.friction);
                        //     //jt=max(jt,-j*friction)
                        //     jt = Mathf.Max(jt, -j * friction);
                        //     //jt=min(jt,j*friction)
                        //     jt = Mathf.Min(jt, j * friction);
                        //     //va'=va-jn/ma
                        //     //write to object
                        //     //cubes[i].rb.velocity=new Vector3(0.2f,0,0);
                        //     cubes[i].rb.velocity = (b.rb.velocity - jt * s.collisionNormal.normalized / b.rb.mass)* b.rb.timer;
                        //    // if (b.name == "Box")
                        //    // {
                        //        cubes[i].rb.velocity.y=0;
                        //        cubes[i].rb.transform.position +=   cubes[i].rb.velocity ;
                        //       //  Debug.Log("cube old vel " + b.rb.velocity + " " + b.rb.velocity.magnitude);

                        //         Debug.Log("cube new vel " + cubes[i].rb.velocity + ";mag: " + cubes[i].rb.velocity.magnitude + ";ms: " + s.mass + ";mb: " + b.rb.mass + ";rs: " + s.restitution + ";rb: " + b.rb.restitution + ";fs: " + s.friction + ";fb: " + b.rb.friction + ";col " + s.collisionNormal + " " + s.collisionNormal.magnitude+"; timer: "+b.rb.timer);

                        //         spheres[k].vel = s.vel - jt * s.collisionNormal.normalized / s.mass;

                        //    // }
                        // }
                        // else
                        // {
                        //     cubes[i].rb.velocity =( b.rb.velocity - s.collisionNormal.normalized / b.rb.mass)* b.rb.timer;
                        // }
                       // Debug.Log("cube old vel " + b.rb.velocity + " " + b.rb.velocity.magnitude);

                   // }
                }

            }
        }


    }
public static void CheckAABBCube(BulletBehaviour s, CubeBehaviour b)//, out bool result)
    {
       
        if ((s.min.x <= b.max.x && s.max.x >= b.min.x) &&
          (s.min.y <= b.max.y && s.max.y >= b.min.y) &&
          (s.min.z <= b.max.z && s.max.z >= b.min.z))
        {
            Debug.Log("bullet and box");
            // determine the distances between the contact extents
            float[] distances = {
                (b.max.x - s.min.x),
                (s.max.x - b.min.x),
                (b.max.y - s.min.y),
                (s.max.y - b.min.y),
                (b.max.z - s.min.z),
                (s.max.z - b.min.z)
            };

            float penetration = float.MaxValue;
            Vector3 face = Vector3.zero;

            // check each face to see if it is the one that connected
            for (int i = 0; i < 6; i++)
            {
                if (distances[i] < penetration)
                {
                    // determine the penetration distance
                    penetration = distances[i];
                    face = faces[i];
                }
            }
           
            s.penetration = penetration;
            s.collisionNormal = face;
            
            Reflect1(s);
        }
    }
    private static void Reflect1(BulletBehaviour s)
    {
        s.transform.position -= s.collisionNormal * s.penetration * s.speed; // resolution
        
        if ((s.collisionNormal == Vector3.forward) || (s.collisionNormal == Vector3.back))
        {
            s.direction = new Vector3(s.direction.x, s.direction.y, -s.direction.z);
        }
        else if ((s.collisionNormal == Vector3.right) || (s.collisionNormal == Vector3.left))
        {
            s.direction = new Vector3(-s.direction.x, s.direction.y, s.direction.z);
        }
        else if ((s.collisionNormal == Vector3.up) || (s.collisionNormal == Vector3.down))
        {
            s.direction = new Vector3(s.direction.x, -s.direction.y, s.direction.z);
        }
    }

    public static void CheckSphereAABB(BulletBehaviour s, CubeBehaviour b)//, out bool result)
    {
       // result = false;
        // get box closest point to sphere center by clamping
        var x = Mathf.Max(b.min.x, Mathf.Min(s.transform.position.x, b.max.x));
        var y = Mathf.Max(b.min.y, Mathf.Min(s.transform.position.y, b.max.y));
        var z = Mathf.Max(b.min.z, Mathf.Min(s.transform.position.z, b.max.z));

        var distance = Math.Sqrt((x - s.transform.position.x) * (x - s.transform.position.x) +
                                 (y - s.transform.position.y) * (y - s.transform.position.y) +
                                 (z - s.transform.position.z) * (z - s.transform.position.z));

        if ((distance < s.radius) && (!s.isColliding))
        {

            // determine the distances between the contact extents
            float[] distances = {
                (b.max.x - s.transform.position.x),
                (s.transform.position.x - b.min.x),
                (b.max.y - s.transform.position.y),
                (s.transform.position.y - b.min.y),
                (b.max.z - s.transform.position.z),
                (s.transform.position.z - b.min.z)
            };

            float penetration = float.MaxValue;
            Vector3 face = Vector3.zero;

            // check each face to see if it is the one that connected
            for (int i = 0; i < 6; i++)
            {
                if (distances[i] < penetration)
                {
                    // determine the penetration distance
                    penetration = distances[i];
                    face = faces[i];
                }
            }

            s.penetration = penetration;
            s.collisionNormal = face;
            b.collisionNormal = face;

         //   result = true;


            Reflect(s);
        }

    }

    // This helper function reflects the bullet when it hits an AABB face
    private static void Reflect(BulletBehaviour s)
    {
        if ((s.collisionNormal == Vector3.forward) || (s.collisionNormal == Vector3.back))
        {
            s.direction = new Vector3(s.direction.x, s.direction.y, -s.direction.z);
        }
        else if ((s.collisionNormal == Vector3.right) || (s.collisionNormal == Vector3.left))
        {
            s.direction = new Vector3(-s.direction.x, s.direction.y, s.direction.z);
        }
        else if ((s.collisionNormal == Vector3.up) || (s.collisionNormal == Vector3.down))
        {
            s.direction = new Vector3(s.direction.x, -s.direction.y, s.direction.z);
        }
    }


    public static void CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        //collision manifold
        Contact contactB = new Contact(b);
//Debug.Log("Cube"+a.min.x+" "+b.min.x+" "+a.max.x+" "+b.max.x);
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            // determine the distances between the contact extents
            //collision manifold
            float[] distances = {
                (b.max.x - a.min.x),
                (a.max.x - b.min.x),
                (b.max.y - a.min.y),
                (a.max.y - b.min.y),
                (b.max.z - a.min.z),
                (a.max.z - b.min.z)
            };

            float penetration = float.MaxValue;
            Vector3 face = Vector3.zero;

            // check each face to see if it is the one that connected
            for (int i = 0; i < 6; i++)
            {
                if (distances[i] < penetration)
                {
                    // determine the penetration distance
                    penetration = distances[i];
                    face = faces[i];
                }
            }

            // set the contact properties
            contactB.face = face;
            contactB.penetration = penetration;

            //bool NotRemove=true;
            // check if contact does not exist
            if (!a.contacts.Contains(contactB))
            {
                // remove any contact that matches the name but not other parameters
                for (int i = a.contacts.Count - 1; i > -1; i--)
                {
                    if (a.contacts[i].cube.name.Equals(contactB.cube.name))
                    {
                        a.contacts.RemoveAt(i);
                        //NotRemove=false;
                    }
                }
                if(a.name =="Player" && b.rb.bodyType==BodyType.DYNAMIC)
                {                                      
                   

                    // a = F/m
                    Vector3 impulse = new Vector3(a.GetComponent<PlayerBehaviour>().F * a.rb.velocity.normalized.x / b.rb.mass, 0.0f,a.GetComponent<PlayerBehaviour>().F * a.rb.velocity.normalized.z / b.rb.mass);
                    b.transform.position +=a.rb.velocity.normalized *  contactB.penetration; 
                    b.rb.velocity = impulse * Time.deltaTime;

                    //move the box by player
                  /*  if (a.name == "Player" && b.name=="Box" && b.gameObject.GetComponent<RigidBody3D>().bodyType == BodyType.DYNAMIC)
                    {                 
                        Debug.Log("collision with player and box");                           
                        if (contactB.face == Vector3.left )
                        {
                             //Debug.Log("collision with player and box left");
                        b.gameObject.transform.position += new Vector3(0, 0, Camera.main.transform.forward.z+1)  * penetration;
                        b.gameObject.GetComponent<RigidBody3D>().velocity = new Vector3( 0, 0,Camera.main.transform.forward.z+1) * Time.deltaTime;
                        }
                        else if (contactB.face == Vector3.right )
                        {
                        b.gameObject.transform.position += new Vector3(0, 0, Camera.main.transform.forward.z-1) *Time.deltaTime* penetration;
                        b.gameObject.GetComponent<RigidBody3D>().velocity = new Vector3(0, 0, Camera.main.transform.forward.z-1) * Time.deltaTime;
                        }

                        else if(contactB.face == Vector3.forward)
                        {
                        b.gameObject.transform.position += new Vector3(Camera.main.transform.forward.x-1, 0, 0) * penetration;
                        b.gameObject.GetComponent<RigidBody3D>().velocity = new Vector3(Camera.main.transform.forward.x-1, 0, 0) * Time.deltaTime;
                        }
                        else if(contactB.face == Vector3.back)
                        {
                        b.gameObject.transform.position += new Vector3(Camera.main.transform.forward.x+1, 0, 0) * penetration;
                        b.gameObject.GetComponent<RigidBody3D>().velocity = new Vector3(Camera.main.transform.forward.x+1, 0, 0) * Time.deltaTime;
                        }
                       
                    }
                    else if (b.gameObject.GetComponent<RigidBody3D>().bodyType == BodyType.DYNAMIC && a.name != "Player" && b.name != "Player")
                    {
                       
                        if (contactB.face == Vector3.forward || contactB.face == Vector3.back || contactB.face == Vector3.left
                            || contactB.face == Vector3.right)
                        {
                            
                            b.gameObject.GetComponent<RigidBody3D>().velocity = contactB.face *Time.deltaTime;
                          //  b.gameObject.GetComponent<RigidBody3D>().velocity.y=0;
                        }
                    }*/
                                                           
                }
                else if(a.name =="Player" && b.name=="Stair" && b.rb.bodyType==BodyType.STATIC )
                {
                    //stair collision
                    Debug.Log("stair collision");
                    if (contactB.face == Vector3.forward || contactB.face == Vector3.back || contactB.face == Vector3.left
                        || contactB.face == Vector3.right)
                    {
                        var velocity = new Vector3(-contactB.face.x, Time.deltaTime, -contactB.face.z);
                        a.gameObject.transform.position += velocity * Time.deltaTime;
                    }
                    if (contactB.face == Vector3.down)
                        {
                            //a.gameObject.transform.position=b.rb.transform.position+new Vector3(0,1,0);
                            Debug.Log("ok");
                        }
                }

                if (contactB.face == Vector3.down)
                {
                    a.gameObject.GetComponent<RigidBody3D>().Stop();
                    a.isGrounded = true;
                }



                // add the new contact
                a.contacts.Add(contactB);
                a.isColliding = true;

            }
        }
        else
        {

            if (a.contacts.Exists(x => x.cube.gameObject.name == b.gameObject.name))
            {
                a.contacts.Remove(a.contacts.Find(x => x.cube.gameObject.name.Equals(b.gameObject.name)));
                a.isColliding = false;

                if (a.gameObject.GetComponent<RigidBody3D>().bodyType == BodyType.DYNAMIC)
                {
                    a.gameObject.GetComponent<RigidBody3D>().isFalling = true;
                    a.isGrounded = false;
                }
            }
        }
    }
}
