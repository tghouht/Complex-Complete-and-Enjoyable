using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    private List<String> tags;

    private PlayerMotor playerMotor;

    public void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    public void Update()
    {

    }

    public void OnCollisionStay(Collision collision)
    {
        if (tags.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = true;
        playerMotor.collisionPoint = collision.contacts[0].point;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (tags.Contains(collision.gameObject.tag)) return;

        playerMotor.isTouching = false;
    }
}