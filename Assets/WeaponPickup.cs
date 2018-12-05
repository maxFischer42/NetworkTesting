using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour {

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
            displayBox.SetActive(false);
            GetComponentInParent<Player>().CmdSwapWeapons(other.GetComponent<Drop>().myWeapon, other.gameObject.transform.parent.gameObject);
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

        return _newWeapon.name;
    }


}


