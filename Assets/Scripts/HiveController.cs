using UnityEngine;
using System.Collections;

public class HiveController : MonoBehaviour
{
	public GameObject hivememberprefab;

	public float health;
	public static int numhivemembers = 5;
	public static ArrayList listofplayers = new ArrayList();
	private static GameObject[] listofhivemembers = new GameObject[numhivemembers];

	void Start()
	{
		//Spawn the hive members.
		for(int iterator = 0; iterator < numhivemembers; iterator++)
		{
			Vector3 spawndistancefromhive = new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5));
			listofhivemembers[iterator] = (GameObject)Instantiate(hivememberprefab, gameObject.transform.position + spawndistancefromhive, gameObject.transform.rotation);
		}
	}

	void Update()
	{
		UpdateListOfPlayers();
	}

	public void UpdateListOfPlayers()
	{
		GameObject[] gameobjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		for(int i = 0; i < gameobjects.Length; i++)
		{
			if (gameobjects[i].CompareTag("Player"))
			{
				listofplayers.Add(gameobjects[i]);
			}
		}
	}
}