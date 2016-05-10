using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGravity : MonoBehaviour {


	[HideInInspector]
	public List<PlanetGravity> nearPlanets = new List<PlanetGravity>();

	Rigidbody bodyrigid;
    [SerializeField]
	private LayerMask ground;
	
	void Start(){
		bodyrigid = GetComponent<Rigidbody> ();
		bodyrigid.useGravity = false;
		bodyrigid.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate () {
		if(nearPlanets.Count == 0)
			return;
		
		Transform nearestPlanet = null;
		float smallestDistance = int.MaxValue;
		Vector3 force = Vector3.zero;
		foreach (PlanetGravity planet in nearPlanets){
			//Gets gravity vector of planet
			force += planet.GetGravityForce(bodyrigid);

			//Distance to each nearby Planet
			RaycastHit ray = new RaycastHit();
			Physics.Raycast(transform.position, (planet.transform.position - transform.position).normalized, out ray, float.MaxValue, ground.value);

			//Keeps Track of nearest planet
			if(ray.distance < smallestDistance){
				smallestDistance = ray.distance;
				nearestPlanet = planet.transform;
			}
		}

		//Angles player towards nearest Planet
		Vector3 gravityUp = (transform.position - nearestPlanet.position).normalized;
		Quaternion rotation = Quaternion.FromToRotation(transform.up,gravityUp) * transform.rotation;
		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.fixedDeltaTime * 2f);

		//Applies Gravity
		bodyrigid.AddForce(force);
	}
}