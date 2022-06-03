using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{

    /*
     * Diese Klasse ist eine Singelton-Klasse welche die Option zur verfügung stellt zwei Objekte toggeln zu können
     */
    public class UIToggler : MonoBehaviour
    {
        private static UIToggler _instance = null;
        public static UIToggler Instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }

        }


        public void ToggelUIButtons(GameObject uiObject, GameObject uiObject2)
        {

            if (uiObject.activeSelf)
            {
                uiObject.SetActive(false);
                uiObject2.SetActive(true);
            }
            else
            {
                uiObject.SetActive(true);
                uiObject2.SetActive(false);
            }
        }
    }
}

