using UnityEngine.Networking;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public void Start()
    {
        //Only permit server to handle collisions of bullets
        if (isClient) GetComponent<Collider>().isTrigger = true;
    }

    public void Update()
    {

    }
}