using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    public float lookSensitivity = 3f;
    public bool isGrounded = true;
    public float jumpAcceleration;
    public float groundCheckDistance = 0.01f;

    public PlayerAnimator animator;

    private LineRenderer ln;

    private Rigidbody rb;

    private PlayerMotor motor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        motor = GetComponent<PlayerMotor>();
        ln = GetComponent<LineRenderer>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        motor.Move(_velocity);

        //Caluclate rotaton as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Caluclate Camera rotaton as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotation = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotation);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        animator.grounded = isGrounded;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            Vector3 _velocity = new Vector3(0, jumpAcceleration, 0);
            rb.AddForce(_velocity * Time.fixedDeltaTime);
        } 
    }



}
