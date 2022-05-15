using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthDemo
{
    public class ObjectDetection : MonoBehaviour
    {
        public bool IsObjectSeen { get; private set; } = false;

        [SerializeField] private Camera cam;
        [SerializeField] private string objectTag;

        private GameObject[] objectsFound = new GameObject[0];

        void Update()
        {
            if (SystemInfo.deviceType != DeviceType.Handheld) return;

            if (objectsFound.Length <= 0)
            {
                SearchObject();
                return;
            }


            foreach (GameObject searchedObject in objectsFound)
            {
                Renderer render = searchedObject.GetComponent<Renderer>();
                if (render.isVisible)
                {
                    IsObjectSeen = CheckRendererInSigth(searchedObject.transform.position);
                    return;
                }
                else
                {
                    IsObjectSeen = false;
                }
            } 
        }

        private bool CheckRendererInSigth(Vector3 objectPosition)
        {
            RaycastHit hit;
            if (Physics.Linecast(cam.transform.position, objectPosition, out hit))
            {
                if (hit.transform.CompareTag(objectTag))
                {
                    return true;

                }
            }
            return false;
        }

        private void SearchObject()
        {
            objectsFound = GameObject.FindGameObjectsWithTag(objectTag);
        }
    }
}
