using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class HiderPlayerController : MonoBehaviourPun
    {
        [SerializeField]
        private GameObject face;
        [SerializeField]
        private CharacterController characterController;

        [Header("PlayerMovement")]

        public float movementSpeed = 2;
        public float runSpeed = 4;
        public float sneakSpeed = 1;
        public float jumpHeight = 1f;

        [Header("Physics")]
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private float gravity = -9.81f;
        [SerializeField]
        private float groundCheckDistance = 0.2f;
        [SerializeField]
        private Camera myCam;

        private PhotonView pv;
        private Vector3 velocity;
        private bool isHiderOnGround = true;
        private bool isWASDPressed = false;

        public bool isMovementDisabled { get; set; } = false;

        private void Start()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
            {
                myCam.gameObject.SetActive(true);
                pv = photonView;
            }
        }

        void Update()
        {
            HandelPlayerMovement();
        }

        void HandelPlayerMovement()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
            {
                if (isMovementDisabled) return;
                if (Input.GetKey(KeyCode.F))
                {
                    Interact();
                }

                //Get inputvalues
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 moveDirection = transform.right * x + transform.forward * z;

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    isWASDPressed = true;
                }
                else
                {
                    isWASDPressed = false;
                    pv.RPC("StopAudio", RpcTarget.All);
                }

                if (Input.GetKey(KeyCode.LeftShift) && isWASDPressed)
                {
                    MoveCharacter(moveDirection, runSpeed);
                    pv.RPC("SetAudioRemote", RpcTarget.All, "Running");
                }
                else if (Input.GetKey(KeyCode.CapsLock) && isWASDPressed)
                {
                    MoveCharacter(moveDirection, sneakSpeed);
                    pv.RPC("SetAudioRemote", RpcTarget.All, "Sneaking");
                }
                else if (isWASDPressed)
                {
                    MoveCharacter(moveDirection, movementSpeed);
                    pv.RPC("SetAudioRemote", RpcTarget.All, "Walking");
                }

                HandleGravity(velocity);

                characterController.Move(velocity * Time.deltaTime);
            }
        }

        void Interact()
        {
            RaycastHit hit;

            if (Physics.Raycast(face.transform.position, face.transform.forward, out hit, 0.5f))
            {

                if (hit.collider.TryGetComponent(out IInteractable interact))
                {
                    interact.OnInteract(gameObject);
                }
            }
        }

        void HandleGravity(Vector3 currentVelocity)
        {
            isHiderOnGround = Physics.CheckSphere(groundCheck.position, groundCheckDistance);

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
                pv.RPC("StopAudio", RpcTarget.All);
                pv.RPC("SetAudioRemote", RpcTarget.All, "Jumping");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isHiderOnGround = false;

            }
        }

        void MoveCharacter(Vector3 moveDirection, float moveSpeed)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }


        [PunRPC]
        public void ChangeToInvis(bool isInvisible)
        {
            gameObject.layer = isInvisible ?  11: 6;
        }
    }
}
