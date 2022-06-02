using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthLib;

namespace StealthDemo
{
    public class HiderPlayerController : HiderMovementFunctions
    {
        [Header("PlayerMovement")]
        public float movementSpeed = 2;
        public float runSpeed = 4;
        public float sneakSpeed = 1;

        [Header("Buttons")]

        [SerializeField] private KeyCode runButton = KeyCode.LeftShift;
        [SerializeField] private KeyCode sneakButton = KeyCode.CapsLock;
        [SerializeField] private KeyCode interactButton = KeyCode.F;
        [SerializeField] private KeyCode jumpButton = KeyCode.Space;

        [Header("Others")]
        [SerializeField]private Camera myCam;

        public bool isMovementDisabled { get; set; } = false;
        private UITask[] tasks;

        private void Start()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
            {
                myCam.gameObject.SetActive(true);
                tasks = FindObjectsOfType<UITask>();
            }
        }

        void FixedUpdate()
        {
            HandelPlayerMovement();
        }

        void HandelPlayerMovement()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
            {
                if (CheckIfUIOpen()) return;

                if (Input.GetKey(interactButton))
                {
                    Interact();
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    MoveCharacter();
                }
                else
                {
                    photonView.RPC("StopAudioRemote", RpcTarget.All);
                }


                HandleGravity(velocity);

                if (Input.GetKey(jumpButton))
                {
                    Jump();
                }

                characterController.Move(velocity * Time.deltaTime);
            }
        }

        private bool CheckIfUIOpen()
        {
            foreach(UITask task in tasks)
            {
                if (task.isUIOpen)
                {
                    return true;
                }
            }
            return false;
        }

        private void MoveCharacter()
        {
            if (Input.GetKey(runButton))
            {
                Move(runSpeed);
                photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.All, "Running");
                return;
            }
            if (Input.GetKey(sneakButton))
            {
                Move(sneakSpeed);
                photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.All, "Sneaking");
                return;
            }

            Move(movementSpeed);
            photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.All, "Walking");
        }

        public override void Jump()
        {
            base.Jump();
            if(isHiderOnGround && Input.GetKey(jumpButton))
            {
                photonView.RPC("StopAudioRemote", RpcTarget.All);
                photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.All, "Jumping");
            }
        }

        [PunRPC]
        public void ChangeToInvis(bool isInvisible)
        {
            gameObject.layer = isInvisible ?  11: 6;
        }
    }
}
