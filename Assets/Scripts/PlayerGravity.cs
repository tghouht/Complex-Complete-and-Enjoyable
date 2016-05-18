using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGravity : MonoBehaviour {


	[HideInInspector]
	public List<PlanetGravity> nearPlanets = new List<PlanetGravity>();
    //	[HideInInspector]
    public bool inSpace;

	Rigidbody bodyrigid;
    PlayerMotor motor;

    [SerializeField]
	private LayerMask ground;
	
	void Start()
    {
		bodyrigid = GetComponent<Rigidbody> ();
        motor = GetComponent<PlayerMotor>();
		bodyrigid.useGravity = false;
		bodyrigid.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate () {
		if(nearPlanets.Count == 0)
			return;
		
		Transform nearestPlanet = null;
		float smallestDistance = (float) int.MaxValue;
        PlanetGravity nearestPlanet0 = null;
		Vector3 force = Vector3.zero;
		foreach (PlanetGravity planet in nearPlanets) {
			//Gets gravity vector of planet
			force += planet.GetGravityForce(bodyrigid);

			//Distance to each nearby Planet
			RaycastHit ray = new RaycastHit();
			Physics.Raycast(transform.position, (planet.transform.position - transform.position).normalized, out ray, float.MaxValue, ground.value);

			//Keeps Track of nearest planet
			if(ray.distance < smallestDistance){
				smallestDistance = ray.distance;
				nearestPlanet = planet.transform;
                nearestPlanet0 = planet;
			}
		}


        //Angles player towards nearest Planet
        Vector3 gravityUp = (transform.position - nearestPlanet.position).normalized;
        Quaternion rotation = Quaternion.FromToRotation(transform.up,gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.fixedDeltaTime * 2f);


		bodyrigid.AddForce(minimizeForce(force));
	}

    private Vector3 minimizeForce(Vector3 force)
    {
        if (force.magnitude / bodyrigid.mass >= 1f)
        {
            minimizeForce(force / 2f);
        }
        else
        {
            return force;
        }

        return force;
    }
}