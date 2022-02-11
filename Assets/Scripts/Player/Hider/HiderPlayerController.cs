using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderPlayerController : MonoBehaviour
{

    public Transform groundCheck;
    public CharacterController characterController;
    [Header("PlayerMovement")]
    public float movementSpeed = 0.4f;
    public float jumpHeight = 0.2f;
    [Header("Physics")]
    public float gravity = -3.905f;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundLayer;

    private Vector3 velocity;
    private bool isHiderOnGround = true;

    // Update is called once per frame
    void Update()
    {
        isHiderOnGround = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayer);

        if(isHiderOnGround && velocity.y < 0 ) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        if (isHiderOnGround && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isHiderOnGround = false;
        }

        
        velocity.y += gravity * Time.deltaTime;
        Debug.Log(velocity.y);

        characterController.Move(velocity * Time.deltaTime);
    }
}
