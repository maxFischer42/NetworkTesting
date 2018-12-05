using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

    public Weapon myWeapon;
    public enum ammoTypes { small, medium, large};
    public ammoTypes ammodrop;
    public int ammoAmount;
}
