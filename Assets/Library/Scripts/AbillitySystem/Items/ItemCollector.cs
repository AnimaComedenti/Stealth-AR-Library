using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Diese Klasse ist für die Verarbeitung des Aufsammelna der Items zuständig.
     * Hierbei werden gefundene Items in einer Liste hinzugeffügt, auf welche von außen zugegriffen werden kann.
     * 
     * Items: Liste der aufgehobenen Items
     * OnDuplicate: Ein Event das bei einem doppeltem Item ausgeführt wird
     */
    public abstract class ItemCollector : MonoBehaviour
    {
        //Item-Liste die von außen genutzt werden kann.
        public List<AbillitySO> Items { get; private set; } = new List<AbillitySO> { };
        public event Action<AbillitySO> OnDuplicated;
        /*
        * Diese Methode ist für das hinzufügen der Items in die Abillity-Liste zuständig.
        * Hierbei wird geprüft ob das Item bereits in der Liste ist, falls ja wird die Anzahl der Aktivierungen hoch gezählt anstatt ein neues Item hinzuzufügen.
        * 
        * itemBehaviour: Der Behavior aus dem das Item genommen wird
        */
        public void AddItemToList(ItemBehaviour itemBehaviour)
        {

            if (Items.Count <= 0) {
                Items.Add(itemBehaviour.Abillity);
                return;
            }

            foreach (AbillitySO item in Items)
            {
                if (item == itemBehaviour.Abillity)
                {
                    OnDuplicated?.Invoke(item);
                    return;
                }
            }
            Items.Add(itemBehaviour.Abillity);
        }

        /*
         * Methode um vorhandene Items in der Liste zu entfernen
         * 
         * abillity: Die Fähigkeit welche aus der Liste genommen werden soll
         */
        public void RemoveItemFromList(AbillitySO abillity)
        {
            Items.Remove(abillity);
        }

        /*
         * Methode um das Item zu zerstören, wenn es aufgesammelt wurde
         * 
         * itemBehavior: Das ItemBehaviour aus dem das Item entnommen werden soll.
         */
        public void AddItemAndDestroy(ItemBehaviour itemBehaviour)
        {
            AddItemToList(itemBehaviour);
            Destroy(itemBehaviour.gameObject);
        }

        #region Overide-Methods
        public abstract void SearchItem();
        #endregion
    }
}