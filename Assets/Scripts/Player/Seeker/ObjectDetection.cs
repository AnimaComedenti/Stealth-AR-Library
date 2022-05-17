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


            if (!IsInCameraSigth())
            {
                IsObjectSeen = false;
                return;
            }

            foreach (GameObject searchedObject in objectsFound)
            {
                Renderer render = searchedObject.GetComponent<Renderer>();
                if (render.isVisible)
                {
                    CheckRendererInSigth(searchedObject.transform.position);
                    return;
                }
                else
                {
                    IsObjectSeen = false;
                }
            } 
        }

        private void CheckRendererInSigth(Vector3 objectPosition)
        {
            RaycastHit hit;
            if (Physics.Linecast(cam.transform.position, objectPosition, out hit))
            {
                if (hit.transform.CompareTag(objectTag))
                {
                    IsObjectSeen = true;
                    return;
                }
            }
            IsObjectSeen = false;
        }

        private void SearchObject()
        {
            objectsFound = GameObject.FindGameObjectsWithTag(objectTag);
        }

        private bool IsInCameraSigth()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
            Collider objCollider = GetComponent<Collider>();

            if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
            {
                return true;
            }

            return false;
        }
    }
}
