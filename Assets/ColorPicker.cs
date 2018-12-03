using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour {

    public Color currentColor;
    public SpriteRenderer colorObj;
    public Material[] materials;


    // Update is called once per frame
    void Update () {
        currentColor = colorObj.color;
	}

    public void M4()
    {
        materials[0].color = currentColor;
    }

    public void KF()
    {
        materials[1].color = currentColor;
    }

    public void P9()
    {
        materials[2].color = currentColor;
    }

    public void PI()
    {
        materials[3].color = currentColor;
    }

    public void RE()
    {
        materials[4].color = currentColor;
    }

    public void SN()
    {
        materials[5].color = currentColor;
    }

    public void SH()
    {
        materials[6].color = currentColor;
    }

    public void SM()
    {
        materials[7].color = currentColor;
    }

    public void RP()
    {
        materials[8].color = currentColor;
    }
}
