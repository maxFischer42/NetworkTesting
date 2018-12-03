using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    [SerializeField]
    private PlayerAnimator PlayerAnimator;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets a movement vector
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets a rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets a rotation vector for the camera
    public void RotateCamera(float _cameraRotation)
    {
        cameraRotationX = _cameraRotation;
    }

    //run every physics iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.z))
        {
            PlayerAnimator.groundSpeed = Mathf.Abs(velocity.x);
        }
        else if(Mathf.Abs(velocity.z) > Mathf.Abs(velocity.x))
        {
            PlayerAnimator.groundSpeed = Mathf.Abs(velocity.z);
        }
        else
        {
            PlayerAnimator.groundSpeed = 0f;
        }
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            
        }
    }

    //Perform Rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(m_camera != null)
        {
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            m_camera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

        }
    }




}
