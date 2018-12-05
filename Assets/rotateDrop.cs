using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateDrop : MonoBehaviour {
    [SerializeField]
    private Vector3 rotationAmp;

	void Update () {
        transform.Rotate(rotationAmp.x,rotationAmp.y,rotationAmp.z);
	}
}
