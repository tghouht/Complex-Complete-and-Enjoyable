using UnityEngine;
using UnityEngine.Networking;

public class Deathbox : MonoBehaviour
{


    public void Start()
    {

    }

    public void Update()
    {

    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Player"))
        {
            collider.transform.GetComponent<PlayerManager>().Die();
            Debug.Log(collider.name + " has run into the deathbox!");
            //collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}