using UnityEngine;

//[System.Serializable]
[CreateAssetMenu]
public class Weapon : ScriptableObject
{

    public string Name = "Glock";
    public Vector3 Position;
    public Vector3 Rotation;
    public int damage = 10;
    public float range = 100f;
    public GameObject graphics;
    public float fireRate = 0f;
    public int ID = 0;
	
}
