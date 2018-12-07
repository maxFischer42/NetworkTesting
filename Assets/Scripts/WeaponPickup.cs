using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WeaponPickup : NetworkBehaviour {

    private WeaponManager weaponManager;
    public Text displayText;
    public GameObject displayBox;

	void Start () {
        weaponManager = GetComponentInParent<WeaponManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Detect")
        {
            string text = GetWeaponInfo(other.GetComponent<Drop>().myWeapon);
            displayText.text = "Press E to recycle for " + text + ".";
            displayBox.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Detect" && Input.GetButtonDown("Equip"))
        {            
            if(GetComponentInParent<NetworkIdentity>().isLocalPlayer)
            {
                GetComponentInParent<WeaponManager>().EquipWeapon(other.GetComponent<Drop>().myWeapon);
                displayBox.SetActive(false);                
                other.GetComponent<Drop>().Used = true;
            }
            else
            {
                GetComponentInParent<Player>().CheckPickup(other.GetComponent<Drop>().myWeapon, other.gameObject);
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Detect")
        {
            displayBox.SetActive(false);
        }
    }

    string GetWeaponInfo(Weapon _weapon)
    {
        Weapon _newWeapon = _weapon;

        return _newWeapon.Name;
    }


}


