using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float MaxHealth = 100f;
	public float health = 0f;
	public GameObject HealthBar;

	// Use this for initialization
	void Start () {
		health = MaxHealth;
		InvokeRepeating ("decreaseHealth", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void decreaseHealth(){
		health -= 23;
		float calcHealth = health / MaxHealth;
		SetHealthBar (calcHealth);
	}
	public void SetHealthBar(float myHealth){
		HealthBar.transform.localScale = new Vector3 (Mathf.Clamp(myHealth,0f,1f), HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
	}
}
