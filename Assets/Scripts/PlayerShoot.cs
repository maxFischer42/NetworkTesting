using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{

    private const string PLAYER_TAG = "Player";



    private Weapon currentWeapon;

    [SerializeField]
    private Camera m_camera;

    private WeaponManager weaponManager;

    private void Start()
    {
        if(!m_camera)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();

    }

    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if(currentWeapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
        
    }

    //is called on the server when a player shoots
    [Command]
    void CmdOnShoot ()
    {
        RpcDoShootEffect();
    }

    //is called on all clients when we need to do
    // a shoot effect
    [ClientRpc]
    void RpcDoShootEffect()
    {
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
    }




    [Client]
    void Shoot()
    {
        if (!isLocalPlayer)
            return;

        CmdOnShoot();
        //Debug.Log("SHOOT");

        RaycastHit _hit;
        if(Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out _hit, currentWeapon.range, weaponManager.layerMask))
        {
            //We hit something
            //Debug.Log("We hit" + _hit.collider.name);
            if(_hit.collider.tag == "Player")
            {
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _ID, int _damage)
    {
        Debug.Log(_ID + " has been shot.");

        Player _player = GameManager.GetPlayer(_ID);
        _player.TakeDamage(_damage, currentWeapon.ID, gameObject.name);
    }

}
