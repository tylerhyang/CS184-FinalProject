using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float moveSpeed;
    public float jumpHeight;
    public float gravityScale = 0;

    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var move = new Vector3(0f, 0f, 0f);
        if (x != 0 || z != 0)
        {
            var targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        
        // move.y = 20.0f *( Time.deltaTime);
        // move.x *= moveSpeed * Time.deltaTime;
        // move.z *= moveSpeed * Time.deltaTime;
        // controller.Move(move);
        // UNCOMMENT FOR ORIGINAL FUNCTIONALITY AND SHIFT PLAYER SPEED TO 12 AND GRAVITY TO 2
        move.y = -0.1f;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * gravityScale);
        }
        _velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
