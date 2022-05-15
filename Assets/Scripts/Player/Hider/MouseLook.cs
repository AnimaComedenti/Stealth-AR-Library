using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthDemo
{
    public class MouseLook : MonoBehaviour
    {

        public float mouseSensitivity = 100f;
        public Transform playerBody;

        private float xRotation = 0f;
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
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