using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public static string ipAddress = "";

    private NetworkManager networkManager;

	public AudioSource music;

    //Establish all sceney stuff
    public void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        try
        {

            if (ipAddress.Equals("") || ipAddress.ToLower().Equals("enter text...") || ipAddress.ToLower().Equals("ip address (leave blank for host)"))
            {
                networkManager.StartHost();
            }
            else
            {
                networkManager.networkAddress = ipAddress;

                NetworkClient networkClient = networkManager.StartClient();

                StartCoroutine(lolCat(networkClient));
            }
			music.Play();
        }
        catch
        {

        }

        if (!networkManager.isNetworkActive)
        {
            ExitScene();
        }
    }

    private IEnumerator lolCat(NetworkClient networkClient)
    {
        yield return new WaitForSeconds(1f);

        if (networkClient.connection == null || !networkClient.isConnected)
        {
            ExitScene();
        }
    }

    private void ExitScene()
    {
        Debug.Log("Apparently the client is not connected");
        ipAddress = "";
        GameManager.instance = null;
        GameManager.UnRegisterAll();
        networkManager.StopClient();
        networkManager.StopHost();
        networkManager.StopServer();
        networkManager.networkAddress = null;
        SceneManager.LoadScene(0);
    }
}