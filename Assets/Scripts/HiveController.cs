using UnityEngine;
using System.Collections;

public class HiveController : MonoBehaviour
{

	public GameObject hivememberprefab;

	public static int numhivemembers = 5;
	private static ArrayList listofplayers = new ArrayList();
	private static GameObject[] listofhivemembers = new GameObject[numhivemembers];

	void Start()
	{

		//spawn the hive members
		for(int iterator = 0; iterator < numhivemembers; iterator++)
		{
			Vector3 spawndistancefromhive = new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5));
			listofhivemembers[iterator] = (GameObject)Instantiate(hivememberprefab, gameObject.transform.position + spawndistancefromhive, gameObject.transform.rotation);
			print ("yea");
		}
	}

	void Update()
	{
		UpdateListOfPlayers ();
	}

	public void UpdateListOfPlayers()
	{
		GameObject[] gameobjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		int iterator = 0;
		for(int i = 0; i < gameobjects.Length; i++)
		{
			if (gameobjects [i].CompareTag("Player"))
			{
				listofplayers[iterator] = (gameobjects[i]);
			}
		}
	}
}