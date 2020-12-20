using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;
     public RigidBody3D[] rb;
    public float F;//player push force
    bool StartSimulatin=false;
    void start()
    {
        F=12.8f;        
        StartSimulatin=false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(StartSimulatin)
        {
        _Fire();
        _Move();
        }
        _StartScene();
        _Activate();
    }
     /*void OnGUI()
     {
          if (GUI.Button(new Rect(100, 0, 150, 30), "Back to Main Menu"))
        {            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
     }*/
      private void _Activate()
      {
        if (Input.GetKeyDown(KeyCode.S))
        {

          StartSimulatin=!StartSimulatin;        
        }
      }
    private void _StartScene()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;         
           UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
    private void _Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y


            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity = transform.up * speed * 0.05f * Time.deltaTime;
            }

            transform.position += body.velocity;
        }
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
