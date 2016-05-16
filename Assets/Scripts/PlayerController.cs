using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float jumpPower = 100f;

    private PlayerMotor playerMotor;

	// Use this for initialization
	void Start ()
    {
		playerMotor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        /**
        Jumping
         */
        bool jump = Input.GetButtonDown("Jump") && playerMotor.isGrounded;
        playerMotor.Jump(jump ? transform.up * jumpPower : Vector3.zero);

        /**
        Movement
         */
		float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 horiMove = transform.right * xMov;
        Vector3 vertMove = transform.forward * zMov;

        Vector3 velocity = (horiMove + vertMove).normalized * speed;

        playerMotor.Move(velocity);

        /**
        Rotation for camera
         */
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotation = xRot * lookSensitivity;

        playerMotor.RotateCamera(cameraRotation);

        /**
        Rotation for player
         */
        float yRot = Input.GetAxisRaw("Mouse X");

        float bodyRotation = yRot * lookSensitivity;

        playerMotor.Rotate(new Vector3(0f, bodyRotation, 0f));
	}
}
