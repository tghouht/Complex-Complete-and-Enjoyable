using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] disabledComponents;
    [SerializeField]
    public GameObject graphicals;
    [SerializeField]
    string remoteLayer;

    Camera sceneCamera;
    [SerializeField]
    Camera playerCamera;

    private PlayerManager playerManager;

    void Start ()
    {
        gameObject.name = "Player " + GetComponent<NetworkIdentity>().netId;
        playerManager.Setup(this);

        if (!isLocalPlayer)
        {
            foreach (Behaviour behaviour in disabledComponents)
            {
                behaviour.enabled = false;
            }
            playerManager.livesBar.gameObject.SetActive(false);
            playerManager.healthBar.gameObject.SetActive(false);

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
    }

    void RemoteLayerAdd()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayer);
    }

    void Update()
    {

    }

    public void HasPermDeathed()
    {
        Debug.Log(transform.name + " has permanently died!");
        playerCamera.gameObject.SetActive(false);
        sceneCamera.gameObject.SetActive(true);
        graphicals.SetActive(false);
    }

    void OnDisable()
    {
        //sceneCamera is null for !isLocalPlayer, never set in Start() method
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);

        if (isLocalPlayer)
        {
            Debug.Log("OnDisable");

            SceneChanger.ipAddress = "";
            GameManager.UnRegisterAll();
            SceneManager.LoadScene(0);
        }

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