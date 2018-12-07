using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour {

    public Weapon myWeapon;
    public enum ammoTypes { small, medium, large };
    public ammoTypes ammodrop;
    public int ammoAmount;
    [SyncVar]
    public bool Used;

    private void Update()
    {
        if(Used)
        {
            gameObject.SetActive(false);
        }        
    }






}
