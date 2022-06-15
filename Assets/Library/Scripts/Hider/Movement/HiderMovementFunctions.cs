using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    /*
     * Grundlegende Bewegungsoptionen für den Spieler
     * 
     * characterController: Der CharacterController welcher für das Bewegen aufgerufen wird
     * jumpHeight: Springhöhe, die bestimmt wie hoch der Spieler springen kann
     * groundCheck: Wird genutzt umm zu erkennen ob der Spieler sich auf dem Boden befindet
     * checkRaduis: Der Radius für die Bodenerkennung
     * groundmask: LayerMask für die Bodenerkennung
     * gravity: Gravitation zum berechnen des Springens
     * 
     * Code Inspired by Brackeys
     * https://www.youtube.com/watch?v=_QajrabyTJc&t=1234s
     */
    public class HiderMovementFunctions : MonoBehaviourPun
    {

        [SerializeField]
        protected CharacterController characterController;

        [Header("Physics")]
        public float jumpHeight = 1;
        [SerializeField]
        protected Transform groundCheck;
        [SerializeField]
        protected float checkRaduis = 0.2f;
        [SerializeField]
        protected LayerMask groundmask;
        [SerializeField]
        protected float gravity = -9.81f;

        protected Vector3 velocity;
        protected bool isHiderOnGround = true;

        //Mothode die das Bewegen bestimmt
        public virtual void Move(float speed)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.right * x + transform.forward * z;
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }

        //Methode die das Springen ermöglicht
        public virtual void Jump()
        {
            if (isHiderOnGround)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isHiderOnGround = false;
            }
        }


        /*
         * Methode zur berechnung der Gravitation
         * 
         * currentVelocity: Die Geschwindigkeit welche im Moment auf dem Spieler wirkt
         */
        public virtual void HandleGravity(Vector3 currentVelocity)
        {
            isHiderOnGround = Physics.CheckSphere(groundCheck.position, checkRaduis,groundmask);

            if (isHiderOnGround && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
        }


        /*
         * Methode welche den Spieler es ermöglich mit Sachen zu interagieren
         */
        protected virtual void Interact()
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