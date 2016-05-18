using UnityEngine.Networking;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon playerWeapon;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject gunParticle;
    [SerializeField]
    private LayerMask mask;

    void Start ()
    {
        if (cam == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, playerWeapon.range, mask))
        {
            if (hit.transform.tag.Equals("Player"))
            {
                Debug.Log("Fired at " + hit.transform.name);
                CmdGotShot(hit.transform.name, playerWeapon.dmg);
            }

            Debug.DrawLine(cam.transform.position, hit.transform.position);
            CmdLocationShot(hit.point, hit.normal);
        }
    }

    [Command]
    public void CmdGotShot(string id, float dmg)
    {
        Debug.Log(id + " has been shot.");

        PlayerManager player = GameManager.GetPlayer(id);
        player.RpcDamaged(dmg);
    }

    [Command]
    void CmdLocationShot(Vector3 pos, Vector3 normal)
    {
        RpcLocationShot(pos, normal);
    }

    [ClientRpc]
    void RpcLocationShot(Vector3 pos, Vector3 normal)
    {
        GameObject gm = (GameObject) Instantiate(gunParticle, pos, Quaternion.LookRotation(normal));
        gm.transform.localScale *= 0.1f;
        Destroy(gm, 1f);
    }
}
