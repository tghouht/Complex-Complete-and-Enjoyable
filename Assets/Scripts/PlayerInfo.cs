using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour
{
    [SyncVar]
    public Transform cameraTransform;

    public void Start()
    {
    }

    public void Update()
    {
    }
}