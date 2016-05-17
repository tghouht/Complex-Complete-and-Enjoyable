using UnityEngine.Networking;

[System.Serializable]
public class PlayerWeapon
{
    [SyncVar]
    public string name = "Laser";
    [SyncVar]
    public float dmg = 5f;
    [SyncVar]
    public float range = 50f;

    void Start ()
    {

    }

    void Update()
    {

    }
}