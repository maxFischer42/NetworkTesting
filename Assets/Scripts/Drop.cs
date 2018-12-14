using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour {

    public Weapon myWeapon;
    public enum ammoTypes { small, medium, large };
    public ammoTypes ammodrop;
    public int ammoAmount;

    

    public void dDestroy()
    {
        Destroy(gameObject);
    }

    [ClientRpc]
    public void RpcDes()
    {
        Destroy(gameObject);
    }

    public void Start()
    {
        int i = Random.Range(0, GameObject.Find("_GameManager").GetComponent<GameManager>().availableWeapons.Length - 1);
        myWeapon = GameObject.Find("_GameManager").GetComponent<GameManager>().availableWeapons[i];
    }




}
