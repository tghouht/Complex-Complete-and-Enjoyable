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

    public void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    public void Start()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1f, groundLayer.value))
        {
            //DebugConfig.print("Found raycast DOWN - " + groundLayer.ToString());
            playerMotor.isGrounded = true;
        }
        else
        {
            //DebugConfig.print("No");
            playerMotor.isGrounded = false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (specialCollisionIgnore.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = true;
        playerMotor.collisionPoint = collision.contacts[0].point;

        DebugConfig.print("OnCollisionStay true for - " + collision.gameObject.name);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (specialCollisionIgnore.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = false;

        DebugConfig.print("OnCollisionExit true for - " + collision.gameObject.name);
    }
}