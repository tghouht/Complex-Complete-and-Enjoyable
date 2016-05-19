using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	public Transform Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Player);
	}
}
