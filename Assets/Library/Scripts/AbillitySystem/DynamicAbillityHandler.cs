using System.Collections;
using UnityEngine;

namespace StealthLib
{
    /*
     * Der DynamicAbillityHandler erweitert den AbillityHandler und ermöglicht es dynamisch Items hinzuzufügen oder löschen zu können.
     * Somit ist diese Klasse nicht an einer Fähigkeit (Abillity) gebunden und kann diese per Runtime bestimmen.
     * Hierbei lauscht diese Klasse die Liste des ItemCollectors und nimmt sich aus diesem die Items heraus.
     * 
     * itemCollector: Der ItemCollector welcher über die Item-Likste verfügt und beobachtet wird
     * itemSlot: Die Positions auf welche Stelle der Item-Liste gelauscht wird.
     */
    public class DynamicAbillityHandler : AbillityHandler
    {
        [SerializeField] protected ItemCollector itemCollector;
        [SerializeField] protected int itemSlot;

        private void Start()
        {
            //Binden des Duplicate-Events
            itemCollector.OnDuplicated += OnDuplicateAbility;
        }

        protected override void FixedUpdate()
        {
            if (itemCollector.Items.Count <=  itemSlot) return;

            if (Abillity == null)
            {
                Abillity = itemCollector.Items[itemSlot];
                itemCollector.RemoveItemFromList(Abillity);
            }

            base.FixedUpdate();
        }
        protected override void ResetDefault()
        {
            base.ResetDefault();

            if (abillitySO == null && itemCollector.Items.Count > itemSlot) itemCollector.RemoveItemFromList(itemCollector.Items[itemSlot]);
        }

        //Falls ein Duplicate gefunden wurde wird das Item wieder Gesetzt.
        //Ist das Item gleich so wird anhand der Setter-Methode "Abillity" der AktivierungsCount der Abillity hochgezählt anstatt dieses neu hinzuzufügen 
        private void OnDuplicateAbility(AbillitySO item)
        {
            if (abillitySO == null || abillitySO != item) return;
            Abillity = item;
        }


        #region Getter & Setter
        public ItemCollector ItemCollector { get => itemCollector; set => itemCollector = value; }
        public int ItemSlot { get => itemSlot; set => itemSlot = value; }
        #endregion
    }
}