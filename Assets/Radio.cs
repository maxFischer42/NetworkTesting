using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

public class Radio : NetworkBehaviour {

    public AudioSource player;
    public AudioClip song;
    public bool playerIn;
    public GameObject driver;
    public GameObject dummy;
    public Transform exit;


	
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Backspace))
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i] == driver)
                {
                    CmdTurnOn();
                    break;
                }
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            CmdExitCar(driver);
        }

	}

    [Command]
    public void CmdExitCar(GameObject _player)
    {
        RpcEnterCar(_player);
    }

    [ClientRpc]
    public void RpcEnterCar(GameObject _player)
    {
        
        _player.SetActive(true);
        _player.transform.SetPositionAndRotation(exit.position, exit.rotation);
        if (!_player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            dummy.GetComponent<CarController>().enabled = true;
        }
        else
        {
            dummy.GetComponent<CarController>().enabled = true;
        }
        dummy.SetActive(false);
    }

    [Command]
    public void CmdTurnOn()
    {
        player.enabled = true;
        RpcTurnOn();
    }

    [ClientRpc]
    public void RpcTurnOn()
    {
        player.enabled = true;
    }
}
