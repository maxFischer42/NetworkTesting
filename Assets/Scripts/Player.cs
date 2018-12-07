using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 10;

    [SyncVar]
    private int currentHealth;

    private int id;
    private string pid;


    private void Awake()
    {
        SetDefaults();
    }

    public void TakeDamage(int amount, int weaponID, string playerID)
    {
        currentHealth -= amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");
        id = weaponID;
        pid = playerID;
    }

    private void Update()
    {
        if (currentHealth <= 0)
            CmdKillPlayer(id, pid);
    }


    [Command]
    void CmdKillPlayer(int weaponID, string playerID)
    {
        GameObject.Find("_GameManager").GetComponent<Killfeed>().RpcKillFeed(playerID, gameObject.name, weaponID);
        Debug.Log("Kill Player");
        RpcKill();
    }
    [ClientRpc]
    void RpcKill()
    {
       gameObject.SetActive(false);
    }




    public void CheckPickup(Weapon wep, GameObject obj)
    {
        
        Debug.Log("Weapon picked up");
        CmdSwapWeapons(wep);
    }

    [Command]
    public void CmdSwapWeapons(Weapon _weapon)
    {
        Debug.Log("Swap Weapons called");
        RpcSwap(_weapon);
    }

    [ClientRpc]
    public void RpcSwap(Weapon _weapon)
    {
        GetComponent<WeaponManager>().EquipWeapon(_weapon);
    }




    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }


}
