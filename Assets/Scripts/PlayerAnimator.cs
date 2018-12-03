using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    public Animator animator;
    public float groundSpeed, yVelocity;
    public bool grounded, sprinting;
    private Rigidbody m_rigidbody;

	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
		if(groundSpeed > 0)
        {
            animator.SetFloat("groundSpeed", 1);
            if(Input.GetButton("Sprint"))
            {
                animator.SetBool("Sprinting",true);
            }
            else
            {
                animator.SetBool("Sprinting", false);
            }
        }
        else
        {
            animator.SetFloat("groundSpeed", 0);
        }

        if(grounded)
        {
            animator.SetBool("grounded", true);
        }
        else
        {
            if(m_rigidbody.velocity.y > 0)
            {
                animator.SetFloat("yVelocity", 1);
            }
            else
            {
                animator.SetFloat("yVelocity", 2);
            }
        }
    }
}
