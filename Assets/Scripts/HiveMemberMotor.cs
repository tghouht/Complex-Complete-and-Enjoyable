using UnityEngine;
using System.Collections;

public class HiveMemberMotor : MonoBehaviour
{
	public static float speed = 10f;
	public static float jumppower;
	public float health;
	public GameObject currenttarget;
	public Rigidbody rigidbdy;
	private Vector3 currentdirection;
	private float xmovecomponent;
	private float ymovecomponent;
	private float zmovecomponent;

	void Start()
	{
		rigidbdy = gameObject.GetComponent<Rigidbody>();
	}

	void Update()
	{
		currentdirection = new Vector3 (xmovecomponent, ymovecomponent, zmovecomponent);
		UpdateTarget();
		if(currenttarget == null)
		{
			Idle();
		}
		else
		{
			MoveToward(currenttarget);
			GetComponent<HiveMemberShoot>().Shoot(currenttarget);
		}

	}
	void FixedUpdate()
	{
		//Move the hive member.
		Vector3 localmove = transform.TransformDirection(currentdirection * speed * Time.fixedDeltaTime);
		rigidbdy.MovePosition(transform.position + localmove);
	}

	public void UpdateTarget()
	{
		//Get the closest player.
		for(int iterator = 0; iterator < HiveController.listofplayers.Count; iterator++)
		{
			GameObject temporaryobject = (GameObject) HiveController.listofplayers[iterator];
			if(currenttarget == null)
			{
				//If close enough, set object being checked to the current target. 
				if(Vector3.Distance(temporaryobject.transform.position, gameObject.transform.position) < 10)
				{
					currenttarget = temporaryobject;
				}
			}
			else if(Vector3.Distance(gameObject.transform.position, currenttarget.transform.position) < Vector3.Distance(gameObject.transform.position, temporaryobject.transform.position))
			{
				currenttarget = (GameObject)HiveController.listofplayers[iterator];
			}
		}
	}

	//These are the different kinds of movement that the hive member can do.
	public void MoveToward(GameObject currentplayer)
	{
		Vector3 dir = (currentplayer.transform.position - transform.position).normalized;
		xmovecomponent = dir.x;
		zmovecomponent = dir.z;
	}
	public void Jump()
	{
		rigidbdy.AddForce(transform.up);
	}
	public void Idle()
	{
		if (Random.Range (1, 100) == 1)
		{
			xmovecomponent = (int)Random.Range (-1, 2);
			zmovecomponent = (int)Random.Range (-1, 2);
		}
	}
}