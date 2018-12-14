using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WinCondition : NetworkBehaviour {


    float timer = 0f;
    public GameObject extraction;

    public void Start()
    {
        if (isServer)
        {
            extraction.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if(timer < 30f)
        {
            timer += Time.deltaTime;
            return;
        }
        if(GameObject.FindGameObjectsWithTag("Player").Length <= 2)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i <= players.Length; i++)
            {
                players[i].GetComponent<PlayerHealth>().endgame.text = "EXTRACTION POINT OPEN";
            }
            extraction.SetActive(true);
            enabled = false;
        }
	}
}
