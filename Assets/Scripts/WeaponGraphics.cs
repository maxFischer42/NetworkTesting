using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class WeaponGraphics : MonoBehaviour {

    public ParticleSystem muzzleFlash;
    public GameObject hitEffectPrefab;
    public AudioClip FireSound;
    public AudioClip ReloadSound;
    public Sprite icon;

}
