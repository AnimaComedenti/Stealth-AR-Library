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
    private float movementSpeed = 2;
    [SerializeField]
    private float runSpeed = 4;
    [SerializeField]
    private float sneakSpeed = 1;
    [SerializeField]
    private float jumpHeight = 1f;
    [Header("Physics")]
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float groundCheckDistance = 0.2f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Camera myCam;
    [SerializeField]
    private SoundMaker soundMaker;
    [Header("Sound")]
    [SerializeField] 
    private SoundSO walking;
    [SerializeField] 
    private SoundSO running;
    [SerializeField] 
    private SoundSO sneaking;
    [SerializeField] 
    private SoundSO shooting;
    [SerializeField]
    private SoundSO jumping;
    [SerializeField]
    private SoundSO landing;

    private Vector3 velocity;
    private bool isHiderOnGround = true;
    private bool isWASDPressed = false;
    private bool isFirstTimeLanded = false;
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

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
            {
                isWASDPressed = true;
            }
            else
            {
                isWASDPressed = false;
                soundMaker.StopSound();
            }

            if (Input.GetKey(KeyCode.LeftShift) && isWASDPressed)
            {
                moveCharacter(moveDirection, runSpeed);
                if (isHiderOnGround) soundMaker.Play3DSound(running);
            }
            else if (Input.GetKey(KeyCode.CapsLock) && isWASDPressed) 
            {
                moveCharacter(moveDirection, sneakSpeed);
                if (isHiderOnGround) soundMaker.Play3DSound(sneaking);
            }
            else if(isWASDPressed)
            {
                moveCharacter(moveDirection, movementSpeed);
                if(isHiderOnGround) soundMaker.Play3DSound(walking);
            }

            HandleGravity(velocity);

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    void HandleGravity(Vector3 currentVelocity)
    {
        isHiderOnGround = Physics.CheckSphere(groundCheck.position, groundCheckDistance);


        if (transform.position.y >= jumpHeight) isFirstTimeLanded = true;

        if (isHiderOnGround && isFirstTimeLanded)
        {
            soundMaker.Play3DSound(landing);
            isFirstTimeLanded = false;
        }

        if (isHiderOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        PerformJump();

        velocity.y += gravity * Time.deltaTime;
    }

    void PerformJump()
    {
        if (isHiderOnGround && Input.GetButtonDown("Jump"))
        {
            soundMaker.StopSound();
            soundMaker.Play3DSound(jumping);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isHiderOnGround = false;   
        }
    }

    void moveCharacter(Vector3 moveDirection, float moveSpeed)
    {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
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
