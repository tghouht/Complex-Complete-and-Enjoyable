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

    [SerializeField]
    private float timeLapseSuper = 3f;
    private float superDuration;

    private PlayerMotor playerMotor;
    private PlayerGravity playerGravity;

	// Use this for initialization
	void Start ()
    {
		playerMotor = GetComponent<PlayerMotor>();
        playerGravity = GetComponent<PlayerGravity>();
        superDuration = timeLapseSuper;
	}

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopServer();
        }
        /**
        Jumping and jetpack
         */
        if (Input.GetButtonDown("Jump") && playerMotor.isGrounded)
        {
            playerMotor.Jump(!Cursor.visible ? transform.up * jumpPower : Vector3.zero);
        }
        else
        {
            playerMotor.Jump(Vector3.zero);
        }

        if (Input.GetButton("Jump"))
        {
            superDuration -= Time.deltaTime;

            if (superDuration <= 0f)
            {
                playerMotor.Jump(!Cursor.visible ? transform.up * superJumpPower : Vector3.zero);
                superDuration = timeLapseSuper;
            }
        }
        else
        {
            superDuration = timeLapseSuper;
        }

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

        playerMotor.Move(!Cursor.visible ? velocity : Vector3.zero);

        /**
        Rotation for camera
         */
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotation = xRot * lookSensitivity;

        playerMotor.RotateCamera(!Cursor.visible ? cameraRotation : 0f);

        /**
        Rotation for player
         */
        float yRot = Input.GetAxisRaw("Mouse X");

        float bodyRotation = yRot * lookSensitivity;

        playerMotor.Rotate(new Vector3(0f, !Cursor.visible ? bodyRotation : 0f, 0f));
	}
}
