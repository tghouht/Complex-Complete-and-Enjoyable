using UnityEngine;
using System.Collections;

public class HiveMemberMotor : MonoBehaviour
{
	public static float speed = .1f;
	public static float jumppower;
	public float health;
	public GameObject currenttarget;
	public Rigidbody rigidbdy;
	public GameObject bulletprefab;
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
		UpdateTarget();
		if(currenttarget == null)
		{
			Idle();
		}
		else
		{
			MoveToward(currenttarget);
			ShootAt(currenttarget);
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
		for(int iterator = 0; iterator < HiveController.listofplayers.Count; i++)
		{
			GameObject temporaryobject = HiveController.listplayers[iterator];
			if(currenttarge == null)
			{
				//If close enough, set 
				if(Vector3.Distance(temporaryobject.transform.position, gameObject.transform.position))
				{
					currenttarget = temporaryobject;
				}
			}
			else if(Vector3.Distance(gameObject.transform.position, currenttarget.transform.position) < Vector3.Distance(gameObject.transform.position, temporaryobject.transform.position))
			{
				currenttarget = (GameObject)listofplayers[i];
			}
		}
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
		if(Random.Range(1,100) == 1)
		{
			xmovecomponent = (int)Random.Range(-1,2);
			zmovecomponent = (int)Random.Range(-1,2);
		}

	}
	public void ShootAt(GameObject target)
	{
		GameObject currentbullet = Instantiate(bulletprefab, gameObject.transform.position, gameObject.transform.rotation);
		currentbullet.GetComponent<Rigidbody>().AddForce((target.transform.position - gameObject.transform.position).normalized);
	}
}