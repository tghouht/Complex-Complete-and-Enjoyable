using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private Behaviour[] disableDeath;

    private bool[] wasEnabled;

    [SyncVar]
    private float health;

    public void Setup ()
    {
        wasEnabled = new bool[disableDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableDeath[i].enabled;
        }

        SetupVars();
    }

    [ClientRpc]
    public void RpcDamaged(float dmg)
    {
        if (isDead) return;

        health -= dmg;

        Debug.Log(transform.name + " now has " + health + " health.");

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.gameSettings.respawnTime);

        SetupVars();

        Transform spawn = NetworkManager.singleton.GetStartPosition();
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;

        Debug.Log(transform.name + " has respawned after " + GameManager.instance.gameSettings.respawnTime + " seconds.");
    }

    //Call this on respawn
    private void SetupVars()
    {
        isDead = false;
        health = maxHealth;

        for (int i = 0; i < wasEnabled.Length; i++)
        {
            disableDeath[i].enabled = wasEnabled[i];
        }

        //GetComponent<Collider>().enabled = true;
    }

    public void Die()
    {
        isDead = true;
        for (int i = 0; i < disableDeath.Length; i++)
        {
            disableDeath[i].enabled = false;
        }

        //GetComponent<Collider>().enabled = false;

        Debug.Log(transform.name + "is now dead.");

        StartCoroutine(Respawn());
    }

    void OnGUI()
    {
        if (!(isDead && isLocalPlayer)) return;

        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        GUILayout.Label("You have died!");

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}