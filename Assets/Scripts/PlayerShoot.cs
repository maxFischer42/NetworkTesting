using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{

    private const string PLAYER_TAG = "Player";

    [SyncVar]
    public float ammo;
    public float maxAmmo;
    public float reloadTime;

    public Text ammoTop;
    public Text ammoBottom;

    private Weapon currentWeapon;

    [SerializeField]
    private Camera m_camera;

    public GameObject[] particles;

    private WeaponManager weaponManager;

    private void Start()
    {
        if (!m_camera)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();

    }

    private void Update()
    {
        ammoTop.text = ammo.ToString();
        ammoBottom.text = maxAmmo.ToString();
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (currentWeapon == null)
            return;
        if (ammo <= 0)
        {
            CancelInvoke("Shoot");
            if (Input.GetButtonDown("Reload"))
            {
                IEnumerator reload = Reload();
                StartCoroutine(reload);
            }
            return;
        }
        if (currentWeapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }


    }


    private IEnumerator Reload()
    {
        weaponManager.GetCurrentGraphics().GetComponent<AudioSource>().PlayOneShot(weaponManager.GetCurrentGraphics().ReloadSound);
        yield return new WaitForSeconds(reloadTime);
        ammo = currentWeapon.ammoClip;
        maxAmmo -= currentWeapon.ammoClip;
        if (maxAmmo <= 0)
            ammo += maxAmmo;
    }



    //is called on the server when a player shoots
    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    //is called on all clients when we need to do
    // a shoot effect
    [ClientRpc]
    void RpcDoShootEffect()
    {
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
        weaponManager.GetCurrentGraphics().GetComponent<AudioSource>().PlayOneShot(weaponManager.GetCurrentGraphics().FireSound);      
    }





    [Client]
    void Shoot()
    {
        if (!isLocalPlayer)
            return;
        Debug.Log("We shoot");
        CmdOnShoot();
        
        RaycastHit _hit;
        if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out _hit, currentWeapon.range, weaponManager.layerMask))
        {
            Debug.Log("Raycast");
            if (_hit.collider.tag == "Player")
            {
                Debug.Log("We hit another player");
                //_hit.collider.GetComponent<PlayerHealth>().RpcTakeDamage(currentWeapon.damage, currentWeapon.ID, gameObject.name);
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage, gameObject.name);
                Debug.Log("We sucessfully hit the player.");
                GameObject effect = (GameObject)Instantiate(particles[0], _hit.point, Quaternion.identity);
                Destroy(effect, 5f);
            }
        }
        ammo--;
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage, string _sourceID)
    {
        Debug.Log(_playerID + " has been shot.");

        //PlayerHealth _player = GameManager.GetPlayer(_playerID);
        GameObject.Find(_playerID).GetComponent<PlayerHealth>().RpcTakeDamage(_damage);
    }

}
