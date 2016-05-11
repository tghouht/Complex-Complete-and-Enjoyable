using UnityEngine;
using UnityEngine.Networking;
using System;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : NetworkBehaviour
{

    [SerializeField]
    private Camera camera;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 currentJump;
    private float activeCameraRot;
    private float currentCameraRot;
    private float lastShot = 0f;
    [NonSerialized]
    public Vector3 collisionPoint;
    [NonSerialized]
    public bool isTouching;
    [NonSerialized]
    public bool isGrounded;

    [SerializeField]
    private float cameraRotMax = 85f;
    [SerializeField]
    private float shootDelay = 1f;
    [SerializeField]
    private float bulletStr = 5f;
    [SerializeField]
    private GameObject bullet;

    private Rigidbody rigidbody;

	// Use this for initialization
	public void Start ()
    {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public void FixedUpdate ()
    {
        lastShot += Time.deltaTime;
        DoJump();
		DoMovement();
        DoRotation();
	}

    public void Jump(Vector3 jump)
    {
        currentJump = jump;
    }

    public void Move(Vector3 vel)
    {
		velocity = vel;
    }

    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    //Called on server
    [Command]
    private void CmdShoot()
    {
        GameObject bulletc = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
        bulletc.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletStr);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            Physics.IgnoreCollision(bulletc.GetComponent<Collider>(), obj.transform.GetComponent<Collider>()); //Ignores collisions between bullet and player
        NetworkServer.Spawn(bulletc);
    }

    public void Shoot()
    {
        if (lastShot >= shootDelay)
        {
            lastShot = 0f;
            DebugConfig.print("Just shot a bullet!");
            CmdShoot();
        }
    }

    public void RotateCamera(float camRot)
    {
		activeCameraRot = camRot;
    }

    private void DoJump()
    {
        if (currentJump != Vector3.zero)
        {
            rigidbody.AddForce(currentJump);
        }
    }

	private void DoMovement()
    {
        if (velocity != Vector3.zero)
        {
            if (isTouching)
            {
                Vector3 collRay = (collisionPoint - transform.position).normalized;
                float dot = Vector3.Dot(velocity, collRay);
                if (dot >= 0) //If dot is negative , it has no component along velocity
                {
                    velocity -= dot * collRay; //Subtract component of velocity along collRay from velocity to get movement directed away from collision
                }
            }

            Vector3 movement = transform.position + velocity * Time.fixedDeltaTime;

            rigidbody.MovePosition(movement);
        }
    }

    private void DoRotation()
    {
		transform.Rotate(rotation);

        if (camera != null)
        {
			currentCameraRot -= activeCameraRot;
            currentCameraRot = Mathf.Clamp(currentCameraRot, -cameraRotMax, cameraRotMax);

            camera.transform.localEulerAngles = new Vector3(currentCameraRot, 0, 0);
        }
    }
}
