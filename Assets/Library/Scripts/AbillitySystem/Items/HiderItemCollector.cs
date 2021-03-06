using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Diese Klasse verarbeitet das Aufsammeln der Items für den Hider.
     * Hierbei wird ein OverlapSpeher erzeugt welche Items-Objekte in der nähe in eine Liste schreibt.
     * Aus dieser Liste werden die Items gefiltert und in eine Liste geschrieben, welche von außen zugegriffen werden kann.
     * 
     * pickUpRadius: Der Radius der genutzt wird um ein Item aufzuheben. Wird für den OverlapSphere genutzt.
     */
    public class HiderItemCollector : ItemCollector
    {
        [SerializeField] private float pickUpRadius = 0.5f;

        void FixedUpdate()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                SearchItem();
            }
        }

        /*
         * Erzeugt ein Overlapsphere und iteriert duch die gefunden Objekte zur überprüfen ob ein Item in den Objekten vorhanden ist.
         * Ist ein Item vorhanden, füge es der Liste hinzu.
         */
        public override void SearchItem()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRadius);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out ItemBehaviour itemBehaviour))
                    {
                        AddItemAndDestroy(itemBehaviour);
                        return;
                    }    
                }
            }
        }


        #region Getter & Setter
        public float PickUpRadius { get => pickUpRadius; set => pickUpRadius = value; }
        #endregion
    }
}