using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    public abstract class ItemCollector : MonoBehaviour
    {
        //Item-Liste die von außen genutzt werden kann.
        public List<AbillitySO> Items { get; private set; } = new List<AbillitySO> { };
        public event Action<AbillitySO> OnDuplicated;
        /*
        * Diese Methode ist für das hinzufügen der Items in die Abillity-Liste zuständig.
        * Hierbei wird geprüft ob das Item bereits in der Liste ist, falls ja wird die Anzahl der Aktivierungen hoch gezählt anstatt ein neues Item hinzuzufügen.
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

        public void RemoveItemFromList(AbillitySO abillity)
        {
            Items.Remove(abillity);
        }

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