using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{

    public GameObject Firework;

    public Text endgame;

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public Text healthBar;
    public Text Remote;

    [SerializeField]
    private GameObject deathEffect;

    public GameObject WeaponDrop;



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
        Cursor.visible = false;
    }

    private void Update()
    {
        healthBar.text = currentHealth + "HP";
        AudioSource[] gameObjects = GameObject.FindObjectsOfType<AudioSource>();
        for(int i = 0; i <= gameObjects.Length - 1; i++)
        {
            gameObjects[i].volume = PlayerPrefs.GetFloat("Volume");
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Win();
        }
    }




    public void Win()
    {
        //GameObject.Find("_GameManager").GetComponent<Killfeed>().RpcKillFeed(playerID, gameObject.name, weaponID);
        Debug.Log("Kill Player");
        GameObject win = (GameObject)Instantiate(Firework);
        win.transform.parent = null;
        win.transform.SetPositionAndRotation(transform.position, Firework.transform.rotation);
        gameObject.SetActive(false);
        GameObject itemDrop = (GameObject)Instantiate(WeaponDrop, transform);
        itemDrop.transform.parent = null;
        itemDrop.GetComponent<Drop>().myWeapon = GetComponent<WeaponManager>().GetCurrentWeapon();
        Instantiate(itemDrop.GetComponent<Drop>().myWeapon.graphics, itemDrop.GetComponentInChildren<rotateDrop>().transform);
    }



    public void SetDefaults()
    {
        currentHealth = maxHealth;
        GameManager.registerPlayer(gameObject.name, GetComponent<PlayerHealth>());
    }

}