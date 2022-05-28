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
    public class SeekerItemCollector : MonoBehaviour
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
         * Trifft der Raycast ein Item so wir es überprüft ob es sich wirklich um ein Item handelt und der Liste hinzugefügt
         */
        private void SearchItem()
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

        /*
         * Diese Methode ist für das hinzufügen der Items in die Abillity-Liste zuständig.
         * Hierbei wird geprüft ob das Item bereits in der Liste ist, falls ja wird die Anzahl der Aktivierungen hoch gezählt anstatt ein neues Item hinzuzufügen.
         */
        public void AddItemToList(ItemBehaviour itemBehaviour)
        {
            if (Items.Count <= 0) Items.Add(itemBehaviour.GetAbillity());

            foreach (AbillitySO item in Items)
            {
                if (item == itemBehaviour.GetAbillity())
                {
                    item.ActivationCount += item.ActivationCount;
                    return;
                }
            }
            Items.Add(itemBehaviour.GetAbillity());
        }

        public void RemoveItemFromList(AbillitySO abillity)
        {
            Items.Remove(abillity);
        }
    }
}
}