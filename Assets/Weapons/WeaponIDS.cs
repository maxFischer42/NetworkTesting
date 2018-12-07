/*/////////////////////////////
 * ////////////////////////////
 *         WEAPON IDS
 * ////////////////////////////
 * ////////////////////////////
 * 
 * Nova - 0
 * Sniper - 1
 * Pistol - 2
 * M4 - 3
 * MP5 - 4
 * P90 - 5
 * Revolver - 6
 * Knife - 7
 * RPG - 8
 *  
 */
using UnityEngine;

public class WeaponIDS
{
    public static int[] GetWeaponIds()
    {
        return new int[9] {0,1,2,3,4,5,6,7,8};
    }

    public static string[] GetWeaponNames()
    {
        return new string[9] { "Nova", "Sniper", "Pistol", "M4", "MP5", "P90", "Revolver", "Knife", "RPG" };
    }

    public static Weapon[] GetWeapons()
    {
        Weapon[] _weapons = GameObject.Find("GameManager").GetComponent<GameManager>().availableWeapons;
        return _weapons;
    }

}