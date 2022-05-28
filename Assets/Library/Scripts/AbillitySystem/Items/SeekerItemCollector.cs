using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
    * Diese Klasse verarbeitet das Aufsammeln der Items für den Seeker.
    * Hierbei wird ein Raycast erzeugt welche Items Objekte, die vom Ihm getroffen wurden, in eine Liste schreibt.
    * Diese Liste wird für die außenstehende Objekte zur Verfügung gestellt.
    */
    public class SeekerItemCollector : ItemCollector
    {
        [SerializeField] private Camera cam;

        //Item-Liste die von außen genutzt werden kann.
        public List<AbillitySO> Items { get; private set; } = new List<AbillitySO> { };
        void Update()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                SearchItem();
            }
        }

        /*
         * Diese Methode erkennt einen "Touch" auf den Touchscreen.
         * Hierbei wird anhand des erkannten Touch-Befehles und der Kameraposition des Smartphones ein Raycast gebildet.
         * Trifft der Raycast ein Item so wird überprüft ob es sich dabei wirklich um ein Item handelt, falls Erfolg wird es der Liste hinzugefügt
         */
        public override void SearchItem()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchposition = touch.position;
                RaycastHit hit;
                Ray screenCenter = cam.ScreenPointToRay(touchposition);

                if (Physics.Raycast(screenCenter, out hit))
                {
                    if (hit.collider.TryGetComponent(out ItemBehaviour itemBehaviour))
                    {
                        AddItemToList(itemBehaviour);
                    }
                }
            }
        }


        #region Getter & Setter
        public Camera SeekerCam { get => cam; set => cam = value; }
        #endregion
    }
}