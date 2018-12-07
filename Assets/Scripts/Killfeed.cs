using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Killfeed : AddKillFeed{
    
    [ClientRpc]
    public void RpcKillFeed(string player1, string player2, int weaponID)
    {
        string _box1 = player1;
        string _box2 = player2;
        Sprite _weapon = weaponIcons[weaponID];
        GameObject _notification = (GameObject)Instantiate(KillFeedElement, killFeed.transform);
        _notification.transform.Find("Player1").GetComponent<Text>().text = _box1;
        _notification.transform.Find("Player2").GetComponent<Text>().text = _box2;
        _notification.transform.Find("Weapon").GetComponent<Image>().sprite = _weapon;
        Debug.Log("Added to killfeed");
        Destroy(_notification, notificationTime);
    }
}

public class AddKillFeed : NetworkBehaviour
{
    public Sprite[] weaponIcons;
    [SerializeField]
    public GameObject KillFeedElement;
    public GameObject killFeed;
    public float notificationTime;
}
