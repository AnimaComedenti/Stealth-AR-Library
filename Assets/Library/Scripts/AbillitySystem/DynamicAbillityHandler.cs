using System.Collections;
using UnityEngine;

namespace StealthLib
{
    public class DynamicAbillityHandler : AbillityHandler
    {
        [SerializeField] protected ItemCollector itemCollector;
        [SerializeField] protected int itemSlot;

        protected override void FixedUpdate()
        {
            if (abillitySO == null)
            {
                abillitySO = itemCollector.Items[itemSlot];
            }

            base.FixedUpdate();
        }


        #region Getter & Setter
        public ItemCollector ItemCollector { get => itemCollector; set => itemCollector = value; }
        public int ItemSlot { get => itemSlot; set => itemSlot = value; }
        #endregion
    }
}