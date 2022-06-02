using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace StealthDemo
{
    public class ScaleButtons : MonoBehaviour
    {
        private Transform arSession;

        private void Start()
        {
            arSession = FindObjectOfType<ARSessionOrigin>().transform;
        }
        public void ScaleUp()
        {
            Vector3 originalScale = arSession.localScale;
            Vector3 newScale = originalScale * 0.9f;
            arSession.transform.localScale = newScale;
        }

        public void ScaleDown()
        {
            Vector3 originalScale = arSession.localScale;
            Vector3 newScale = originalScale * 1.1f;
            arSession.transform.localScale = newScale;
        }
    }
}
