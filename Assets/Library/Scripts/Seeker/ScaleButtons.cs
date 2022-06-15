using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace StealthLib
{   
    /*
     * Skalierungs-Buttons zum anpassen der Scene des Seekers
     */
    public class ScaleButtons : MonoBehaviour
    {
        private Transform arSession;

        private void Start()
        {
            arSession = FindObjectOfType<ARSessionOrigin>().transform;
        }

        /*
         * Methode welche die Skalliewrung der Scene per Methodenaufruf schrittweise erhöht
         */
        public void ScaleUp()
        {
            Vector3 originalScale = arSession.localScale;
            Vector3 newScale = originalScale * 0.9f;
            arSession.localScale = newScale;
        }

       /*
        * Methode welche die Skalliewrung der Scene per Methodenaufruf schrittweise verringert
        */
        public void ScaleDown()
        {
            Vector3 originalScale = arSession.localScale;
            Vector3 newScale = originalScale * 1.1f;
            arSession.localScale = newScale;
        }
    }
}
