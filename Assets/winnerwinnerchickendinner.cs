using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winnerwinnerchickendinner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = PlayerPrefs.GetString("Win") + " Wins!";
	}

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 15)
        {
            Application.Quit();
        }
    }


}
