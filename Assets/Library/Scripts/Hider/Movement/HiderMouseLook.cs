using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Script welches das mitdrehen der Kamera sowie des Objektes erlaubt bei der Bewegung des Mauszeigers
     * 
     * mouseSensitivity: Maus Empfindlichkeit, welche die geschwindikeit der Mausbewegung bearbeitet.
     * playerBody: Das Objekt welches sich mit der Mausbewegung mitdrehen soll
     * 
     * Code By Brackeys
     * https://www.youtube.com/watch?v=_QajrabyTJc&t=1234s
     */
    public class HiderMouseLook : MonoBehaviour
    {

        public float mouseSensitivity = 100f;
        public Transform playerBody;

        private float xRotation = 0f;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void LookAroundPlayer()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        private void Update()
        {
            LookAroundPlayer();
        }
    }
}