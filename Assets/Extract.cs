using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Extract : NetworkBehaviour
{

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().endgame.text = "Press E to evacuate!";
            if(Input.GetKeyDown(KeyCode.E))
            {
                CmdExtract(other.gameObject.name);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().endgame.text = "EXTRACTION POINT OPEN";
        }
    }






    [Command]
    public void CmdExtract(string winner)
    {
        RpcLoadWinScreen(winner);
    }

    [ClientRpc]
    public void RpcLoadWinScreen(string winner)
    {
        PlayerPrefs.SetString("Win", winner);
        Cursor.visible = true;
        SceneManager.LoadScene("End");
    }





}
