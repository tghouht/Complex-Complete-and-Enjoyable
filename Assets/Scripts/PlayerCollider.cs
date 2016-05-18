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
        if (Physics.Raycast(transform.position, -transform.up, 0.8f, groundLayer.value))
        {
            playerMotor.isGrounded = true;
        }
        else
        {
            playerMotor.isGrounded = false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (specialCollisionIgnore.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = true;
        playerMotor.collisionPoint = collision.contacts[0].point;

        Debug.Log("OnCollisionStay true for - " + collision.gameObject.name);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (specialCollisionIgnore.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = false;

        Debug.Log("OnCollisionExit true for - " + collision.gameObject.name);
    }
}