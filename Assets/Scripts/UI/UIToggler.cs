using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthARLibrary
{
    public class UIToggler : MonoBehaviour
    {
        [SerializeField] private GameObject ARUI;
        [SerializeField] private GameObject PCUI;
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


            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                ARUI.SetActive(true);
            }
            else
            {
                PCUI.SetActive(true);
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

