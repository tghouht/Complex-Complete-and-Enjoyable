using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGravity : MonoBehaviour {


	[HideInInspector]
	public List<PlanetGravity> nearPlanets = new List<PlanetGravity>();

	Rigidbody bodyrigid;
<<<<<<< HEAD
	FirstPersonController controller;
=======
    [SerializeField]
	private LayerMask ground;
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
	
	void Start(){
		bodyrigid = GetComponent<Rigidbody> ();
		bodyrigid.useGravity = false;
		bodyrigid.constraints = RigidbodyConstraints.FreezeRotation;
<<<<<<< HEAD
		controller = GetComponent<FirstPersonController>();
=======
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d
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
<<<<<<< HEAD
			Physics.Raycast(transform.position,(planet.transform.position - transform.position).normalized,out ray,float.MaxValue,controller.groundedMask);
=======
			Physics.Raycast(transform.position, (planet.transform.position - transform.position).normalized, out ray, float.MaxValue, ground.value);
>>>>>>> 76c1c2787fe7de8e1913343ab88fd4c6bb8d905d

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