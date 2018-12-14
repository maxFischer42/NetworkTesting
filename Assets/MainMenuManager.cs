using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject[] menuList;

    public InputField xSens;
    public InputField ySens;
    public Slider volumeScroll;
    private float localXSens;
    private float localYSens;
    public float volume;


    void Start()
    {
        localXSens = PlayerPrefs.GetFloat("Sens");

        xSens.text = localXSens.ToString();

        volume = PlayerPrefs.GetFloat("Volume");
        volumeScroll.value = volume;
    }

    void Update()
    {
        PlayerPrefs.SetFloat("Volume", volumeScroll.value);
        volume = PlayerPrefs.GetFloat("Volume");

        PlayerPrefs.SetFloat("Sens", float.Parse(xSens.text));
    }

    public void Skin(bool to)
    {
        if(to)
        {
            SceneManager.LoadScene("SkinManager");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToMenu(int menuID)
    {
        menuList[menuID].SetActive(true);
        for (int i = 0; i <= menuList.Length; i++)
        {
            if(i != menuID)
            {
                menuList[i].gameObject.SetActive(false);
            }
        }        
    }


}
