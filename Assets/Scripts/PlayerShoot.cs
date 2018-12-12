using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{

    private const string PLAYER_TAG = "Player";

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
        if(!m_camera)
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
        weaponManager.GetCurrentGraphics().GetComponent<AudioSource>().PlayOneShot(weaponManager.GetCurrentGraphics().FireSound);
    }





    [Client]
    void Shoot()
    {
        if (!isLocalPlayer)
            return;

       
        CmdOnShoot();
        //Debug.Log("SHOOT");
        
       RaycastHit _hit;
       // GetComponent<LineRenderer>().SetPosition(0, transform.position);
        if(Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out _hit, currentWeapon.range, weaponManager.layerMask))
        {
  /*          int i = 0;
            if (_hit.collider.tag == "Ground")
            {
                i = 1;
            }
            else if(_hit.collider.tag == "Road")
            {
                i = 2;
            }
            else if (_hit.collider.tag == "Building")
            {
                i = 3;
            }*/
            //We hit something
            Debug.Log("We hit" + _hit.collider.name);
           // GetComponent<LineRenderer>().SetPosition(1, _hit.point);
            if (_hit.collider.tag == "Player")
            {
                //_hit.collider.GetComponent<Player>().CmdTakeDamage(currentWeapon.damage, gameObject.name, currentWeapon.ID);
                //GameObject.Find("DamageManager").GetComponent<DamageManager>().CmdTakeDamage(currentWeapon.damage, gameObject.name, currentWeapon.ID);
                Player hit = _hit.collider.GetComponent<Player>();
                _hit.collider.GetComponent<Player>().currentHealth -= currentWeapon.damage;
                _hit.collider.GetComponent<Player>().id = currentWeapon.ID;
                hit.pid = gameObject.name;
                
            }
            GameObject effect = (GameObject)Instantiate(particles[0], _hit.point, Quaternion.identity);
            Destroy(effect, 5f);
        }
            ammo--;
    }

                //CmdPlayerShot(_hit.collider.name, currentWeapon.damage);    
   /* [Command]
    void CmdPlayerShot(string _ID, int _damage)
    {
        Debug.Log(_ID + " has been shot.");

        //GameManager.GetPlayer(_ID);
        GameObject.Find(_ID).GetComponent<Player>().TakeDamage(_damage, gameObject.name,currentWeapon.ID);
    }*/

    //[ClientRpc]

}
