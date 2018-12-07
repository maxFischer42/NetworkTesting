using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlightMovement : MonoBehaviour {

    private Vector3 Origin;
    public Vector3 lowerBound;
    public Vector3 UpperBound;
    public float timer;
    private float timer_;
    public bool isOrigin;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = Velocity();
        isOrigin = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer_ += Time.deltaTime;
        if(timer_ >= timer)
        {
            
            if(!isOrigin)
            {
                GetComponent<Rigidbody>().velocity = Velocity();
                timer_ = 0;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = Origin - GetComponent<Rigidbody>().velocity;
            }
            isOrigin = !isOrigin;

        }
	}

    Vector3 Velocity()
    {
        float x = Random.Range(lowerBound.x,UpperBound.x);
        float y = Random.Range(lowerBound.y, UpperBound.y);
        float z = Random.Range(lowerBound.z, UpperBound.z);
        return new Vector3(x, y, z).normalized;
    }

}
