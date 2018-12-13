using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

  //  public Text healthBar;

 //   [SerializeField]
 //   private int maxHealth = 10;

  //  [SyncVar(hook = "OnChangeHealth")]
  //  public int currentHealth = 1;

 //   public Text Remote;

 //   [SyncVar]
  //  public int id;
  //  [SyncVar]
 //   public string pid;


    //public void Start()
    //{
    //    GameManager.registerPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Player>());
    //}
  

  /*  private void Update()
    {
        //healthbar.text =  currentHealth + "HP";
        if (currentHealth <= 0 && !isServer)
            CmdKillPlayer(id, pid);
        Remote.text = currentHealth + "HP";
    }


    [Command]
    public void CmdKillPlayer(int weaponID, string playerID)
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

    [Command]
    public void CmdChangeHealth(int newHP)
    {
        ChangeHealth(newHP);
    }


    [Server]
    public void ChangeHealth(int newHP)
    {
        currentHealth -= newHP;
    }



    [Command]
    public void CmdTakeDamage(int _amount)
    {
        RpcTakeDamage(_amount);
    }


    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            CmdKillPlayer(id,pid);
        }
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
        //currentHealth = maxHealth;
        gameObject.name = GetComponent<NetworkIdentity>().netId.ToString();
   }
   /*
    void OnChangeHealth(int health)
    {
        healthBar.text = currentHealth + " HP";
    }
*/
}
