using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

[System.Serializable]
public class Contact : IEquatable<Contact>
{
    public CubeBehaviour cube;
    public Vector3 face;
    public float penetration;

    public Contact(CubeBehaviour cube)
    {
        this.cube = cube;
        face = Vector3.zero;
        penetration = 0.0f;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Contact objAsContact = obj as Contact;
        if (objAsContact == null) return false;
        else return Equals(objAsContact);
    }

    public override int GetHashCode()
    {
        return this.cube.gameObject.GetInstanceID();
    }

    public bool Equals(Contact other)
    {
        if (other == null) return false;

        return (
            (this.cube.gameObject.name.Equals(other.cube.gameObject.name)) &&
            (this.face == other.face) &&
            (Mathf.Approximately(this.penetration, other.penetration))
            );
    }

    public override string ToString()
    {
        return "Cube Name: " + cube.gameObject.name + " face: " + face.ToString() + " penetration: " + penetration;
    }
}


[System.Serializable]
public class CubeBehaviour : MonoBehaviour
{
    [Header("Cube Attributes")]
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool debug;
    public List<Contact> contacts;
    public Vector3 collisionNormal;

    private MeshFilter meshFilter;
    public Bounds bounds;
    public bool isGrounded;
    public RigidBody3D rb;//= new RigidBody3D();
 //public float timer;
    // Start is called before the first frame update
    void Start()
    {
        // timer = 0.0f;
        debug = false;
        meshFilter = GetComponent<MeshFilter>();
        rb=GetComponent<RigidBody3D>();
        bounds = meshFilter.mesh.bounds;
        size = bounds.size;
       // rb.restitution=0.8f;
      //  rb.friction=0.6f;
        rb.mass=0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // timer += Time.deltaTime;
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
       // Debug.Log("vel c f "+rb.velocity.magnitude);
       // if (contactB.face == Vector3.down)
          //      {
         //           a.gameObject.GetComponent<RigidBody3D>().Stop();
         //           a.isGrounded = true;
          //      }
        //GetComponent<RigidBody3D>().velocity=rb.velocity;
        // if(!float.IsNaN(rb.velocity.x) && !float.IsNaN(rb.velocity.y) &&
        //    !float.IsNaN(rb.velocity.z))
      //   {
       // rb.velocity+=rb.acceleration*Time.deltaTime;
       //if(name == "Box")
          //  {
            //    Debug.Log("new vel"+rb.velocity+" "+rb.velocity*timer);
              //  transform.position+=rb.velocity*(1/60);      
                //isColliding=false;
          //  }
       //  }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
        }
    }

}
