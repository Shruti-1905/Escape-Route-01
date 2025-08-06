// // using UnityEngine;

// // public class IsometricCameraFollow : MonoBehaviour
// // {
// //     public Transform target;   
// //     public Vector3 offset = new Vector3(-10, 10, -10);  
// //     public float followSpeed = 5f;

// //     void LateUpdate()
// //     {
// //         if (target == null) return;

// //         Vector3 desiredPosition = target.position + offset;
// //         transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
// //         transform.LookAt(target);  // Optional: keeps camera focused on player
// //     }
// // }
// // using UnityEngine;

// // public class EyeCamera : MonoBehaviour
// // {
// //     public Transform playerTransform;                 // Reference to player
// //     public Vector3 eyeOffset = new Vector3(0, 1.6f, 0); // Eye height

// //     void LateUpdate()
// //     {
// //         if (playerTransform != null)
// //         {
// //             transform.position = playerTransform.position + playerTransform.TransformVector(eyeOffset);
// //             transform.rotation = playerTransform.rotation;
// //         }
// //     }
// // }

using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float staminaMax = 5f;
    public float staminaDrainRate = 1f;
    public float staminaRegenRate = 1.5f;

    [Header("Look Settings")]
    public float mouseSensitivity = 2f;
    public float minY = -80f;
    public float maxY = 80f;

    private float stamina;
    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stamina = staminaMax;
    }

    void Update()
    {
        LookAround();
        Move();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minY, maxY);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);  // Rotate the parent (if camera is a child)
    }

    void Move()
    {
        float speed = walkSpeed;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        bool isMoving = direction.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && stamina > 0f;

        if (isRunning && isMoving)
        {
            speed = runSpeed;
            stamina -= staminaDrainRate * Time.deltaTime;
        }
        else if (!isRunning)
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0f, staminaMax);

        Vector3 move = transform.TransformDirection(direction.normalized) * speed * Time.deltaTime;
        transform.position += move;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using JetBrains.Rider.Unity.Editor;
// using UnityEngine;

// public class CameraMovement : MonoBehaviour
// {

// public float speed = 15;
// public float lookSpeed = 200;

// private Rigidbody rig;

// void Start ()
// {
// rig = GetComponent<Rigidbody>();
// }
// void Update()
// {
// //movement
// float hAxis = Input.GetAxis("Horizontal");
// float vAxis = Input.GetAxis("Vertical");

// Vector3 hMove = hAxis * transform.right;
// Vector3 vMove = vAxis * transform.up;
// Vector3 Movement = (hMove + vMove).normalized * speed;

// rig.MovePosition (Movement);

// //looking//
// float MousehAxis = Input.GetAxis("Mouse X");

// Vector3 look = new Vector3(0, MousehAxis, 0) * lookSpeed * Time.deltaTime;

// rig.MoveRotation(rig.rotation * Quaternion.Euler(look));
// }
// }