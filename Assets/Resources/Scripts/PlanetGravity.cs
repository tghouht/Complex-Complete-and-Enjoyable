using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
public class PlanetGravity : MonoBehaviour {

	public float density;
	public float scale;
	float mass;
	float volume;
	//SphereCollider groundCollider;

	void Start(){
		/*SphereCollider[] colliders = transform.GetComponents<SphereCollider>();
		foreach(SphereCollider sphere in colliders){
			if(sphere.radius == .5f)
				groundCollider = sphere;
		}*/
		volume = 4f/3f * Mathf.PI * Mathf.Pow(transform.localScale.x,3);
		mass = density * volume;
	}

	public Vector3 GetGravityForce(Rigidbody body) {
		Vector3 vector = (body.position - transform.position).normalized;
		float force = -(PlanetGenerator.gravityConstant * mass * body.mass)/Mathf.Pow((body.position - transform.position).magnitude,2);
		return (vector * force);
	}
	void OnTriggerEnter(Collider obj){
		if (obj.tag == "Player")
			obj.GetComponent<PlayerGravity>().nearPlanets.Add(this);

	}
	void OnTriggerExit(Collider obj){
		if (obj.tag == "Player")
			obj.GetComponent<PlayerGravity>().nearPlanets.Remove(this);
	}
}

[CustomEditor (typeof(PlanetGravity))]
public class PlanetGravityEditor : Editor{

	public override void OnInspectorGUI(){
		PlanetGravity planet = (PlanetGravity)target;

		if(DrawDefaultInspector()){
			planet.transform.localScale.Set(planet.scale,planet.scale,planet.scale);
		}
	}
	
}
