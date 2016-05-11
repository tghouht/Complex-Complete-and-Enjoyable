using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerGravity))]
[RequireComponent (typeof (Rigidbody))]
public class FirstPersonController : MonoBehaviour {

	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 6;
	public float flySpeed = 10;
	public float rotateSpeed = 1;
	public float jumpForce = 500;
	public LayerMask groundedMask;

	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody bodyrigid;
	PlayerGravity playerGravity;


	void Awake() {
		//Lock Mouse to game window
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		cameraTransform = Camera.main.transform;
		bodyrigid = GetComponent<Rigidbody> ();
		playerGravity = GetComponent<PlayerGravity> ();
	}

	void Update() {

		//Allows plaer to release mouse from game by hitting Left Ctrl
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			if (Cursor.lockState == CursorLockMode.Locked)
				Cursor.lockState = CursorLockMode.None;
			else
				Cursor.lockState = CursorLockMode.Locked;

			Cursor.visible = !Cursor.visible;
		}
		if (Cursor.visible)
			return;
		
		//Basc Controlles when near planets
		if (playerGravity.nearPlanets.Count > 0) {
			// Look rotation:
			transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * mouseSensitivityX);
			verticalLookRotation += Input.GetAxis ("Mouse Y") * mouseSensitivityY;
			verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 60);
			cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

			// Calculate movement:
			float inputX = Input.GetAxisRaw ("Horizontal");
			float inputY = Input.GetAxisRaw ("Vertical");
			
			Vector3 moveDir = new Vector3 (inputX, 0, inputY).normalized;
			Vector3 targetMoveAmount = moveDir * walkSpeed;
			moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

			// Jump
			if (Input.GetButtonDown ("Jump"))
				if (IsGrounded ())
					bodyrigid.AddForce (transform.up * jumpForce);
		}
		//Free Flight Controlles. If near no planets
		else {
			// Look rotation:
			transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * mouseSensitivityX);
			transform.Rotate (Vector3.left * Input.GetAxis ("Mouse Y") * mouseSensitivityY);
			verticalLookRotation = Mathf.Lerp(verticalLookRotation,0,.1f);
			cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

			// Calculate movement:
			float inputX = Input.GetAxisRaw ("Horizontal");
			float inputZ = Input.GetAxisRaw ("Vertical2");
			float inputY = Input.GetAxisRaw ("Vertical");

			transform.Rotate (Vector3.forward * Input.GetAxisRaw ("Rotation") * rotateSpeed);

			Vector3 moveDir = new Vector3 (inputX, inputZ, inputY).normalized;
			Vector3 targetMoveAmount = moveDir * flySpeed;
			moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
		}
	}

	bool IsGrounded(){
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		return(Physics.Raycast (ray, out hit, 1 + .1f, groundedMask));
	}

	void FixedUpdate() {
		// Apply movement to bodyrigid
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		bodyrigid.MovePosition(bodyrigid.position + localMove);
	}
}