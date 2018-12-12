using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamageManager : NetworkBehaviour
{

    [Command]
    public void CmdTakeDamage(int amount, string playerID, int weaponID)
    {
        Debug.Log("Did damage on server");
        int currentHealth = GameObject.Find(playerID).GetComponent<Player>().currentHealth;
        currentHealth -= amount;
       //Debug.Log(playerID + " now has " + currentHealth + " health.");
        int id = weaponID;
        string pid = playerID;
        RpcSet(currentHealth, playerID, weaponID);
        Player player = GameObject.Find(playerID).GetComponent<Player>();
        player.currentHealth = currentHealth;
        player.id = id;
        player.pid = pid;
    }

    [ClientRpc]
    public void RpcSet(int damage, string playerID, int weaponID)
    {
        int id = weaponID;
        string pid = playerID;
        Player player = GameObject.Find(playerID).GetComponent<Player>();
        player.currentHealth = damage;
        player.id = id;
        player.pid = pid;
    }
}
