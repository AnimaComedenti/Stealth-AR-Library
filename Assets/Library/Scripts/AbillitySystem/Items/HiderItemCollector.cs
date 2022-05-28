using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Diese Klasse verarbeitet das Aufsammeln der Items für den Hider.
     * Hierbei wird ein OverlapSpeher erzeugt welche Items Objekte in der nähe in eine Liste schreibt.
     * Aus dieser Liste werden die Items gefiltert und in eine Liste geschrieben, welche von außen zugegriffen werden kann.
     */
    public class HiderItemCollector : MonoBehaviour
    {
        [SerializeField] private float pickUpRadius = 0.5f;
        [SerializeField] private GameObject objectToCastFrom;

        //Item-Liste die von außen genutzt werden kann.
        public List<AbillitySO> Items { get; private set; } = new List<AbillitySO> { };
        void Update()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                SearchItem();
            }
        }

        /*
         * Erzeugt ein Overlapsphere und iteriert duch die gefunden Objekte zur überprüfen ob ein Item in den Objekten vorhanden ist.
         */
        private void SearchItem()
        {
            Collider[] colliders = Physics.OverlapSphere(objectToCastFrom.transform.position, pickUpRadius);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out ItemBehaviour itemBehaviour))
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

        public void RemoveItemFromList(AbillitySO item)
        {
            Items.Remove(item);
        }
    }
}