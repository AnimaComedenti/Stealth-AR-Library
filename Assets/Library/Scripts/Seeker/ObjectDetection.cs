using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Klasse die für die Erkennung des Spielers im Sichtwinkel des Gegenspielers zuständig ist
     */
    public class ObjectDetection : MonoBehaviour
    {
        public bool IsObjectSeen { get; private set; } = false;

        [SerializeField] private Camera cam;
        //Layermask des gesuchten Objektes
        [SerializeField] private int objectLayer = 6;
        //Tag des gesuchten Objektes
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
            IsInCameraSight();
        }

        #region ObjectDetection

        /*
         * Methode welche überprüft ob das gesuchte Objekt im blickwinkel der Kamera ist
         */
        private void IsInCameraSight()
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

                CheckObjectInSight(searchedObject.transform.position);
                if (IsObjectSeen) return;
            }
        }


        /*
         * Methode welche überprüft ob das gesuchte Objekt über das gesuchte Layer verfügt
         */
        private void CheckObjectInSight(Vector3 objectPosition)
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
