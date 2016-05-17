using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetGravity : MonoBehaviour {

	//[HideInInspector]
	public float density;
	//[HideInInspector]
	public float scale;
	[HideInInspector]
	float mass;
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

	[SyncVar]
	public float density;
	[SyncVar]
	public float scale;
    [SyncVar]
	float mass;
    [SyncVar]
	float volume;

    void Start(){

        GenerateTerrain();

        //Sets Scale to generated scale
        transform.localScale = new Vector3 (scale, scale, scale);

        //Estimates volume
        volume = 4f/3f * Mathf.PI * Mathf.Pow(scale,3f);

        //Calculate Mass of planet
        mass = density * volume;
    }

    public Vector3 GetGravityForce(Rigidbody body) {
        //Direction to Planet from Player
        Vector3 vector = (body.position - transform.position).normalized;
        //Calculate Gravity force
        float force = -(PlanetGenerator.gravityConstant * mass * body.mass)/(body.position - transform.position).sqrMagnitude;
        return (vector * force);
    }

    void GenerateTerrain(){
        System.Random psuedo = new System.Random(PlanetGenerator.seed.GetHashCode());
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] points = mesh.vertices;
        float radius = points[0].magnitude;

        for(int i = 0;i < points.Length;i++)
            points[i] = points[i].normalized * (radius + (float) psuedo.NextDouble() / 50f);

        mesh.vertices = points;
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
