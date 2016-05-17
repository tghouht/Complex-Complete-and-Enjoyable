using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    private List<String> specialCollisionIgnore;
    [SerializeField]
    private LayerMask groundLayer;

    private PlayerMotor playerMotor;

<<<<<<< HEAD
    public void Start()
=======
    public void Awake()
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

<<<<<<< HEAD
=======
    public void Start()
    {

    }

>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
    public void Update()
    {

    }

    public void FixedUpdate()
    {
<<<<<<< HEAD
        if (Physics.Raycast(transform.position, -transform.up, 0.5f, groundLayer.value))
        {
            //DebugConfig.print("Found raycast DOWN - " + groundLayer.ToString());
=======
        if (Physics.Raycast(transform.position, -transform.up, 1f, groundLayer.value))
        {
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
            playerMotor.isGrounded = true;
        }
        else
        {
<<<<<<< HEAD
            //DebugConfig.print("No");
=======
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
            playerMotor.isGrounded = false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (specialCollisionIgnore.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = true;
        playerMotor.collisionPoint = collision.contacts[0].point;

<<<<<<< HEAD
        DebugConfig.print("OnCollisionStay true for - " + collision.gameObject.name);
=======
        Debug.Log("OnCollisionStay true for - " + collision.gameObject.name);
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
    }

    public void OnCollisionExit(Collision collision)
    {
        if (specialCollisionIgnore.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = false;

<<<<<<< HEAD
        DebugConfig.print("OnCollisionExit true for - " + collision.gameObject.name);
=======
        Debug.Log("OnCollisionExit true for - " + collision.gameObject.name);
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
    }
}