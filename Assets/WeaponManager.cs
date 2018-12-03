using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class WeaponManager : NetworkBehaviour
{

    [SerializeField]
    private string weaponLayerName = "Weapon";
    public LayerMask layerMask;

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private Weapon primaryWeapon;

    private Weapon currentWeapon;
    private WeaponGraphics currentGraphics;

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }


    private void Start()
    {
        EquipWeapon(primaryWeapon);

    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    void EquipWeapon(Weapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponIns = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position, _weapon.graphics.transform.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        if(currentGraphics == null)
        {
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);
        }


        if(isLocalPlayer)  
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));

        
    }


}
