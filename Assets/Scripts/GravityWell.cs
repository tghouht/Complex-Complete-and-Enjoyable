using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour {

	//Add PlanetGravity script to PlayerGravity nearPlanets list
	void OnTriggerEnter(Collider obj){
		if (obj.tag == "Player")
        {
            obj.GetComponent<PlayerGravity>().nearPlanets.Add(transform.GetComponentInParent<PlanetGravity>());
            //obj.GetComponent<PlayerMotor>().jetPackEnabled = false;
        }

	}
	void OnTriggerExit(Collider obj){
		if (obj.tag == "Player")
			obj.GetComponent<PlayerGravity>().nearPlanets.Remove(transform.GetComponentInParent<PlanetGravity>());
	}
}
