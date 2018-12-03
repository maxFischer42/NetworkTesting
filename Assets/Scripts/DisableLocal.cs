using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisableLocal : MonoBehaviour {

    public GameObject select;

	void Start () {
		if(select.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
	}
	
	
}
