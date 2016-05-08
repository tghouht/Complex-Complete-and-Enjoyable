using UnityEngine;
using System.Collections;

public class HiveMemberMotor : MonoBehaviour
{
	public static float speed = .1f;
	public static float jumppower;
	public GameObject currenttarget;
	public Rigidbody rigidbdy;
	public CharacterController charactercontroller;
	public Vector3 currentdirection;
	public float xmovecomponent;
	public float ymovecomponent;
	public float zmovecomponent;

	void Start()
	{
		rigidbdy = gameObject.GetComponent<Rigidbody>();
	}

	void Update()
	{
		currentdirection = new Vector3 (xmovecomponent, ymovecomponent, zmovecomponent);
		Idle ();
	}
	void FixedUpdate()
	{
		//Move the hive member.
		rigidbdy.MovePosition(transform.position + currentdirection * speed);
	}

	public GameObject UpdateTarget()
	{
		//get the closest player
		//for(int i = 0; i < listofplayers.; i++)
		//{
		//	if (!currenttarget.Equals(null))
		//	{
				//if(Vector3.Distance(gameObject.transform.position, currenttarget.transform.position) < Vector3.Distance(gameObject.transform.position, listofplayers[i].transform.position))
				//	currenttarget = (GameObject)listofplayers[i];
		//	}
		//}


		//if there's a player that is attacking the enemy but further away, set that as target
		//if there's another enemy already attacking an enemy and there's another player who is farther away but attacking
		//
		return(null);

	}

	//These methods are being used in order to determine an individual player's worth based on their current situation. The more worth, the more likely it is that the bot will attack the player.
	public float GetDistanceWorth(GameObject player)
	{
		return(1f);
	}
	public float HealthProblem()
	{
		return(1f);
	}


	//These are the different kinds of movement that the hive member can do.
	public void MoveToward(GameObject currentplayer)
	{
		Vector3 dir = currentplayer.transform.position - transform.position;
		xmovecomponent = dir.x;
		zmovecomponent = dir.z;
	}
	public void Jump()
	{
		rigidbdy.AddForce(transform.up);
	}
	public void Idle()
	{
		if(Random.Range(1,100) == 1)
		{
			xmovecomponent = (int)Random.Range(-1,1);
			zmovecomponent = (int)Random.Range(-1,1);
		}
	}
}