using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    public abstract class ItemCollector : MonoBehaviour
    {
        //Item-Liste die von außen genutzt werden kann.
        public List<AbillitySO> Items { get; private set; } = new List<AbillitySO> { };

        /*
        * Diese Methode ist für das hinzufügen der Items in die Abillity-Liste zuständig.
        * Hierbei wird geprüft ob das Item bereits in der Liste ist, falls ja wird die Anzahl der Aktivierungen hoch gezählt anstatt ein neues Item hinzuzufügen.
        */
        public void AddItemToList(ItemBehaviour itemBehaviour)
        {
            if (Items.Count <= 0) Items.Add(itemBehaviour.Abillity);

            foreach (AbillitySO item in Items)
            {
                if (item == itemBehaviour.Abillity)
                {
                    item.ActivationCount += item.ActivationCount;
                    return;
                }
            }
            Items.Add(itemBehaviour.Abillity);
        }

        public void RemoveItemFromList(AbillitySO abillity)
        {
            Items.Remove(abillity);
        }

        #region Overide-Methods
        public abstract void SearchItem();
        #endregion
    }
}