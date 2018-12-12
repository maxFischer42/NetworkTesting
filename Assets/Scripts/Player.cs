using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

    public Text healthBar;

    [SerializeField]
    private int maxHealth = 10;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = 1;

    [SyncVar]
    public int id;
    [SyncVar]
    public string pid;


    private void Awake()
    {
        healthBar.text = currentHealth + " HP";
        SetDefaults();
    }

    //public void Start()
    //{
    //    GameManager.registerPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Player>());
    //}
  

    private void Update()
    {
        //healthbar.text =  currentHealth + "HP";
        if (currentHealth < 0)
            CmdKillPlayer(id, pid);
    }


    [Command]
    void CmdKillPlayer(int weaponID, string playerID)
    {
        GameObject.Find("_GameManager").GetComponent<Killfeed>().RpcKillFeed(playerID, gameObject.name, weaponID);
        Debug.Log("Kill Player");
        RpcKill();
        gameObject.SetActive(false);
    }
    [ClientRpc]
    void RpcKill()
    {
       gameObject.SetActive(false);
    }



/*
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

*/


    public void SetDefaults()
   {
        currentHealth = maxHealth;
        gameObject.name = GetComponent<NetworkIdentity>().netId.ToString();
   }

    void OnChangeHealth(int health)
    {
        healthBar.text = currentHealth + " HP";
    }

}
