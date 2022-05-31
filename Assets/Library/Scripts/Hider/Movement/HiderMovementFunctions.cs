using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    public class HiderMovementFunctions : MonoBehaviourPun
    {

        [SerializeField]
        protected CharacterController characterController;

        [Header("Physics")]
        public float jumpHeight = 1;
        [SerializeField]
        protected Transform groundCheck;
        [SerializeField]
        protected float gravity = -9.81f;
        [SerializeField]
        protected float groundCheckDistance = 0.2f;

        protected Vector3 velocity;
        protected bool isHiderOnGround = true;


        public void Move(float speed)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.right * x + transform.forward * z;
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }

        public void Jump()
        {
            if (isHiderOnGround)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isHiderOnGround = false;
            }
        }


        public void HandleGravity(Vector3 currentVelocity)
        {
            isHiderOnGround = Physics.CheckSphere(groundCheck.position, groundCheckDistance);

            if (isHiderOnGround && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
        }

        protected void Interact()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
            {
                if (hit.collider.TryGetComponent(out IInteractable interact))
                {
                    interact.OnInteract();
                }
            }
        }
    }
}