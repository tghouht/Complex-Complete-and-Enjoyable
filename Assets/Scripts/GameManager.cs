using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public GameSettings gameSettings;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one GameManager in scene!");
        }
        else
        {
            instance = this;
        }
    }

    #region Game management
    public static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();

    public static void RegisterPlayer(string id, PlayerManager player)
    {
        string newId = "Player " + id;
        players.Add(newId, player);
    }

    public static void UnRegisterPlayer(string id)
    {
        players.Remove(id);
    }

    public static void UnRegisterAll()
    {
        players.Clear();
    }

    public static PlayerManager GetPlayer(string id)
    {
        return players[id];
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string id in players.Keys)
        {
            GUILayout.Label(id + " - " + players[id].getHealth());
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    #endregion
}