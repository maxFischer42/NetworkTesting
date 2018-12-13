using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{

    public GameObject Firework;

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public Text healthBar;
    public Text Remote;

    [SerializeField]
    private GameObject deathEffect;




    //void Update()
    //{
    //	if (!isLocalPlayer)
    //		return;

    //	if (Input.GetKeyDown(KeyCode.K))
    //	{
    //		RpcTakeDamage(99999);
    //	}
    //}
    private void Start()
    {        
        SetDefaults();
    }

    private void Update()
    {
        healthBar.text = currentHealth + "HP"; 
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <= 0 && isServer)
        {
            CmdKillPlayer();
        }
        else if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    

    [Command]
    public void CmdKillPlayer()
    {
        //GameObject.Find("_GameManager").GetComponent<Killfeed>().RpcKillFeed(playerID, gameObject.name, weaponID);
        Debug.Log("Kill Player");
        gameObject.SetActive(false);
    }

    [ClientRpc]
    public void RpcWin()
    {
        //GameObject.Find("_GameManager").GetComponent<Killfeed>().RpcKillFeed(playerID, gameObject.name, weaponID);
        Debug.Log("Kill Player");
        GameObject win = (GameObject)Instantiate(Firework);
        win.transform.parent = null;
        win.transform.SetPositionAndRotation(transform.position, Firework.transform.rotation);
        gameObject.SetActive(false);
    }



    public void SetDefaults()
    {
        currentHealth = maxHealth;
        GameManager.registerPlayer(gameObject.name, GetComponent<PlayerHealth>());
    }

}