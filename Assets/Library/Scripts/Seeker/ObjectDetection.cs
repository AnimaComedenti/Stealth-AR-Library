using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    public class ObjectDetection : MonoBehaviour
    {
        public bool IsObjectSeen { get; private set; } = false;

        [SerializeField] private Camera cam;
        [SerializeField] private int objectLayer = 6;
        [SerializeField] private string objectTag = "Player";

        private GameObject[] objectsFound = new GameObject[0];

        void Update()
        {
            if (SystemInfo.deviceType != DeviceType.Handheld) return;

            if (objectsFound.Length <= 0)
            {
                SearchObject();
                return;
            }
            IsInCameraSigth();
        }

        #region ObjectDetection
        private void IsInCameraSigth()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

            foreach (GameObject searchedObject in objectsFound)
            {
                Vector3 objectPosition = searchedObject.transform.position;

                foreach (Plane plane in planes)
                {
                    if (plane.GetDistanceToPoint(objectPosition) < 0)
                    {
                        IsObjectSeen = false;
                        return;
                    }
                }

                CheckRendererInSigth(searchedObject.transform.position);
                if (IsObjectSeen) return;
            }
        }

        private void CheckRendererInSigth(Vector3 objectPosition)
        {
            RaycastHit hit;
            if (Physics.Linecast(cam.transform.position, objectPosition, out hit))
            {
                if (hit.collider.gameObject.layer == objectLayer)
                {
                    Debug.Log("Found Object nothing is in the way");
                    IsObjectSeen = true;
                    return;
                }
            }
            IsObjectSeen = false;
        }
        #endregion

        private void SearchObject()
        {
            objectsFound = GameObject.FindGameObjectsWithTag(objectTag);
        }
    }
}
