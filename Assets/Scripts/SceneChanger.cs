using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static string ipAddress = "";

    private NetworkManager networkManager;

    bool start = false;

    public void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    //Establish all sceney stuff
    public void Start()
    {
        try
        {

            if (ipAddress.Equals("") || ipAddress.ToLower().Equals("enter text...") || ipAddress.ToLower().Equals("ip address (leave blank for host)"))
            {
                networkManager.StartHost();
            }
            else
            {
                networkManager.networkAddress = ipAddress;
                networkManager.StartClient();
            }
        }
        catch
        {
            Debug.Log("Apparently the client is not connected");
            ipAddress = "";
            GameManager.instance = null;
            GameManager.UnRegisterAll();
            networkManager.networkAddress = null;
            SceneManager.LoadScene(0);
            return;
        }

        if (!networkManager.isNetworkActive || !networkManager.client.isConnected)
        {
            Debug.Log("Apparently the client is not connected");
            ipAddress = "";
            GameManager.instance = null;
            GameManager.UnRegisterAll();
            networkManager.networkAddress = null;
            SceneManager.LoadScene(0);
            return;
        }

        start = true;
    }
}