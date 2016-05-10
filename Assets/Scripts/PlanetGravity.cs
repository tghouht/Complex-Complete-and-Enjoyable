﻿using UnityEngine;
using UnityEngine.Networking;

public class PlanetGravity : NetworkBehaviour {

	[SyncVar]
	public float density;
	[SyncVar]
	public float scale;
    [SyncVar]
	float mass;
    [SyncVar]
	float volume;

	void Start(){
		//Sets Scale to generated scale
		transform.localScale = new Vector3 (scale, scale, scale);

		//Estimates volume
		volume = 4f/3f * Mathf.PI * Mathf.Pow(scale,3);
		
		//Calculate Mass of planet
		mass = density * volume;
	}

	public Vector3 GetGravityForce(Rigidbody body) {
		//Direction to Planet from Player
		Vector3 vector = (body.position - transform.position).normalized;
		//Calculate Gravity force
		float force = -(PlanetGenerator.gravityConstant * mass * body.mass)/Mathf.Pow((body.position - transform.position).magnitude,2);
		return (vector * force);
	}
}
