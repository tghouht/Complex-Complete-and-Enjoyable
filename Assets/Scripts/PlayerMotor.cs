using UnityEngine;
using System;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour
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

    public void Shoot()
    {
        if (lastShot >= shootDelay)
        {
            lastShot = 0f;
            DebugConfig.print("Just shot a bullet!");
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
                velocity -= Vector3.Project(velocity, collRay); //Subtract component of velocity along collRay from velocity to get movement directed away from collision
            }

            Vector3 movement = transform.position + velocity * Time.fixedDeltaTime;

            rigidbody.MovePosition(movement);
        }
    }

    private void DoRotation()
    {
		rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotation));

        if (camera != null)
        {
			currentCameraRot -= activeCameraRot;
            currentCameraRot = Mathf.Clamp(currentCameraRot, -cameraRotMax, cameraRotMax);

            camera.transform.localEulerAngles = new Vector3(currentCameraRot, 0, 0);
        }
    }
}
