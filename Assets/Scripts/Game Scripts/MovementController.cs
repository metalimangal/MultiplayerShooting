using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    GameObject fpsCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpDownRotation = 0f;
    private float currentCameraUpDownRotation = 0f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (fpsCamera != null)
        {
            currentCameraUpDownRotation -= CameraUpDownRotation;
            currentCameraUpDownRotation = Mathf.Clamp(currentCameraUpDownRotation, -85, 85);
            fpsCamera.transform.localEulerAngles =new Vector3(currentCameraUpDownRotation, 0, 0);
        }
    }

    void Update()
    {
        //Calculate movement velocity as 3d vector.
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        //Final movement velocity vector

        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;
        Move(_movementVelocity);

        //calculate rotation as 3D vector

        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0,_yRotation,0)*lookSensitivity;

        Rotate(_rotationVector);

        //Calculate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;
        RotateCamera(_cameraUpDownRotation);
    }

    private void RotateCamera(float cameraUpDownRotation)
    {
        CameraUpDownRotation = cameraUpDownRotation;
    }

    private void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    private void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }
    

    
}
