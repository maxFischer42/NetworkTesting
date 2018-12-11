using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class WeaponManager : NetworkBehaviour
{

    public int smallAmmo;
    public int mediumAmmo;
    public int largeAmmo;

    [SerializeField]
    private string weaponLayerName = "Weapon";
    public LayerMask layerMask;

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private Weapon primaryWeapon;

    public Image Equipped;

    private Weapon currentWeapon;
    private WeaponGraphics currentGraphics;


    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }


    private void Start()
    {
    //    EquipWeapon(primaryWeapon);

    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    [Command]
    public void CmdEquipWeapon(int ID)
    {
        RpcEquipWeapon(ID);
    }

    [ClientRpc]
    public void RpcEquipWeapon(int ID)
    {
        Weapon _weapon = FindWeapon(ID);

        primaryWeapon = _weapon;
        if(currentGraphics != null)
        {
            Destroy(currentGraphics.gameObject);
        }
        currentWeapon = primaryWeapon;
        GameObject _gfx = primaryWeapon.graphics;
        Transform _trans = weaponHolder.transform;
        GameObject _weaponIns = (GameObject)Instantiate(_gfx);
        _weaponIns.transform.SetParent(weaponHolder);
        _weaponIns.transform.localPosition = _weapon.Position;
        _weaponIns.transform.localRotation = Quaternion.Euler(_weapon.Rotation);
        GetComponent<PlayerShoot>().ammo = currentWeapon.ammoClip;
        GetComponent<PlayerShoot>().maxAmmo = currentWeapon.givenAmmo;
        GetComponent<PlayerShoot>().reloadTime = currentWeapon.reloadTime;
       // _weaponIns.transform.rotation = Quaternion.Euler(_weapon.Rotation);

        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        if(currentGraphics == null)
        {
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);
        }


        if (isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
            Equipped.sprite = currentGraphics.icon;
        }

        
    }









    public static Weapon FindWeapon(int ID)
    {
        Weapon _weapon = new Weapon();
        for (int i = 0; i < GetWeaponIds().Length; i++)
        {
            if (i == ID)
            {
                _weapon = GetWeapons()[i];
            }
        }
        return _weapon;
    }

    public static Weapon[] GetWeapons()
    {
        Weapon[] _weapons = GameObject.Find("_GameManager").GetComponent<GameManager>().availableWeapons;
        return _weapons;
    }

    public static int[] GetWeaponIds()
    {
        return new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    }

}
