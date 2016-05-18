using UnityEngine;
using System.Collections;

public class HiveMemberShoot : MonoBehaviour
{
	void Start(){}

	void Update()
	{
		
	}
	public void Shoot(GameObject currenttarget)
	{
		RaycastHit hit;
		//If the currenttarget is not null and a raycast hits the player or hivemember...
		if(Physics.Raycast(gameObject.transform.position, (gameObject.transform.position - currenttarget.transform.position).normalized,out hit))
		{
			if(hit.transform.tag.Equals("Player") || hit.transform.tag.Equals("HiveMember"))
			{
				
			}
				//...shoot.
				//hit.transform.gameObject.GetComponent<>()
		}
	}
}