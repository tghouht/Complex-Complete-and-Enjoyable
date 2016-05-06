using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class PlayerGravity : MonoBehaviour {


	[HideInInspector]
	public List<PlanetGravity> nearPlanets = new List<PlanetGravity>();

	Rigidbody bodyrigid;
	
	void Start(){
		bodyrigid = GetComponent<Rigidbody> ();

		bodyrigid.useGravity = false;
		bodyrigid.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate () {
		if(nearPlanets.Count == 0)
			return;
		
		Transform nearestPlanet = null;
		float smallestDistance = float.MaxValue;
		Vector3 force = Vector3.zero;
		foreach (PlanetGravity planet in nearPlanets){
			force += planet.GetGravityForce (bodyrigid);
			float distance = Vector3.Distance(planet.transform.position,transform.position);
			if(distance < smallestDistance){
				smallestDistance = distance;
				nearestPlanet = planet.transform;
			}
		}
		Vector3 gravityUp = (transform.position - nearestPlanet.position).normalized;
		Vector3 localUp = transform.up;
		Quaternion rotation = Quaternion.FromToRotation(localUp,gravityUp) * transform.rotation;

		bodyrigid.AddForce(force);
		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.fixedDeltaTime * 2f);
	}
}