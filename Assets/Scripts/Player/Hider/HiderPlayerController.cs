using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HiderPlayerController : MonoBehaviourPun
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private CharacterController characterController;
    [Header("PlayerMovement")]
    [SerializeField]
    private float movementSpeed = 0.4f;
    [SerializeField]
    private float jumpHeight = 0.2f;
    [Header("Physics")]
    [SerializeField]
    private float gravity = -3.905f;
    [SerializeField]
    private float groundCheckDistance = 0.4f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Camera myCam;

    private Vector3 velocity;
    private bool isHiderOnGround = true;

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
        {
            myCam.gameObject.SetActive(true);
        }
    }
    void HandelPlayerMovement()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
        {
            //Get inputvalues
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.right * x + transform.forward * z;

            characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

            HandleGravity(velocity);

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    void HandleGravity(Vector3 currentVelocity)
    {
        isHiderOnGround = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayer);

        if (isHiderOnGround && velocity.y < 0) velocity.y = -2f;

        PerformJump();

        velocity.y += gravity * Time.deltaTime;
    }

    void PerformJump()
    {
        if (isHiderOnGround && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isHiderOnGround = false;
        }
    }

    void Update()
    {
        HandelPlayerMovement();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
