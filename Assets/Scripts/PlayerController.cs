using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jetPackScalingFactor = 2f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float jumpPower = 100f;
    [SerializeField]
    private float superJumpPower = 1000f;
    private float jumpCounter = 0;

    private PlayerMotor playerMotor;
    private PlayerGravity playerGravity;

	// Use this for initialization
	void Start ()
    {
		playerMotor = GetComponent<PlayerMotor>();
        playerGravity = GetComponent<PlayerGravity>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!PlayerCrosshair.mouseLocked)
        {
            if (Input.GetMouseButton(0))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerCrosshair.mouseLocked = true;
            }

            return;
        }

        /**
        Jumping and jetpack
         */
        if (Input.GetButtonDown("Jump") && jumpCounter < 1)
        {
            playerMotor.Jump(transform.up * jumpPower);
            jumpCounter++;
            print("has jumped counter=" + jumpCounter);
        }
        else if (Input.GetButtonDown("Jump") && jumpCounter == 1)
        {
            //playerMotor.jetPackEnabled = true;
            //print("Jetpack enabled! counter=" + jumpCounter);
            playerMotor.Jump(transform.up * superJumpPower);
            jumpCounter++;
        }
        else
        {
            playerMotor.Jump(Vector3.zero);
        }

        if (playerMotor.isGrounded) jumpCounter = 0;

//        if (Input.GetKeyDown(KeyCode.J))
//        {
//            playerMotor.jetPackEnabled = false;
//            jumpCounter = 0;
//            print("Jetpack disabled!");
//        }

        /**
        Movement
         */
		float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 horiMove = transform.right * xMov;
        Vector3 vertMove = (transform.forward) * zMov;

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
