using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour {

    public Weapon myWeapon;
    public enum ammoTypes { small, medium, large };
    public ammoTypes ammodrop;
    public int ammoAmount;

    
    public void Use()
    {
        CmdDestroy();
    }

    [Command]
    public void CmdDestroy()
    {
        RpcDes();
    }

    [ClientRpc]
    public void RpcDes()
    {
        Destroy(gameObject);
    }





}
