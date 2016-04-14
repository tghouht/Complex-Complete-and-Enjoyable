using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    private Vector3 velocity;
    private Vector3 rotation;
    private float activeCameraRot;
    private float currentCameraRot;

    [SerializeField]
    private float cameraRotMax = 85f;

    private Rigidbody rigidbody;

	// Use this for initialization
	public void Start ()
    {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public void FixedUpdate ()
    {
		DoMovement();
        DoRotation();
	}

    public void Move(Vector3 vel)
    {
		velocity = vel;
    }

    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    public void RotateCamera(float camRot)
    {
		activeCameraRot = camRot;
    }

	private void DoMovement()
    {
        if (velocity != Vector3.zero)
        {
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
