using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

public class CarManager : NetworkBehaviour {

    public GameObject drivable;

	

    public void OnTriggerStay(Collider other)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<NetworkIdentity>().isLocalPlayer && players[i] == other && Input.GetButtonDown("Jump"))
            {
                CmdEnterCar(other.gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    [Command]
    public void CmdEnterCar(GameObject _player)
    {
        RpcEnterCar(_player);
    }

    [ClientRpc]
    public void RpcEnterCar(GameObject _player)
    {
        drivable.SetActive(true);
        drivable.GetComponent<Radio>().playerIn = true;
        drivable.GetComponent<Radio>().driver = _player;
        _player.SetActive(false);
        if(!_player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            drivable.GetComponent<CarController>().enabled = false;
        }
        else
        {
            drivable.GetComponent<CarController>().enabled = true;
        }
    }


}
