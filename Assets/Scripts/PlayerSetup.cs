using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] disabledComponents;
    [SerializeField]
    string remoteLayer;

    Camera sceneCamera;

    private PlayerManager playerManager;

    void Start ()
    {
        if (!isLocalPlayer)
        {
            foreach (Behaviour behaviour in disabledComponents)
            {
                behaviour.enabled = false;
            }

            RemoteLayerAdd();
        }
        else
        {
            sceneCamera = Camera.main;

            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        gameObject.name = "Player " + GetComponent<NetworkIdentity>().netId;
        playerManager.Setup();
    }

    void RemoteLayerAdd()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayer);
    }

    void Update()
    {

    }

    void OnDisable()
    {
        //sceneCamera is null for !isLocalPlayer, never set in Start() method
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        if (isLocalPlayer)
        {
            SceneManager.LoadScene(0);
        }

        GameManager.UnRegisterPlayer(transform.name);

        //Debug.Log(transform.name + "'s setup script has just been disabled w/ camera=" + sceneCamera.gameObject.active);
    }

    /**
    Will be called before Start(), so don't use transform.name for Network ID
     */
    public override void OnStartClient()
    {
        base.OnStartClient();

        playerManager = GetComponent<PlayerManager>();

        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), playerManager);
    }
}