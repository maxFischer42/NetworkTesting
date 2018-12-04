using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Weapon : ScriptableObject
{

    public string name = "Glock";

    public int damage = 10;
    public float range = 100f;
    public GameObject graphics;
    public float fireRate = 0f;
    public int ID = 0;
	
}
