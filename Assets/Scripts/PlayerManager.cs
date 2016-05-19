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

    [SerializeField]
    public GUIBarScript livesBar;
    [SerializeField]
    public GUIBarScript healthBar;

    private bool[] wasEnabled;

    private bool dragged;

    [SyncVar]
    private float health;
    [SyncVar]
    private float lives;

    private PlayerSetup playerSetup;

    public void FixedUpdate()
    {
        if (dragged)
            GetComponent<Rigidbody>().drag -= 1f;
        if (GetComponent<Rigidbody>().drag <= 0)
        {
            GetComponent<Rigidbody>().drag = 0;
            dragged = false;
        }

        livesBar.Value = 0.2f * lives;
        healthBar.Value = health / 100f;
    }

    public void Setup (PlayerSetup playerSetup)
    {
        this.playerSetup = playerSetup;

        wasEnabled = new bool[disableDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableDeath[i].enabled;
        }

        SetupVars();
        lives = 2f;
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

        if (lives <= 0f)
        {
            playerSetup.HasPermDeathed();
        }
        else
        {
            SetupVars();

            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;

            GetComponent<Rigidbody>().drag = 100f;
            dragged = true;

            Debug.Log(transform.name + " has respawned after " + GameManager.instance.gameSettings.respawnTime + " seconds.");
        }
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
        lives--;

        StartCoroutine(Respawn());
    }

    public float getHealth()
    {
        return health;
    }
}