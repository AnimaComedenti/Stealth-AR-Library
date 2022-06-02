using System.Collections;
using UnityEngine;

namespace StealthLib
{
    public class DynamicAbillityHandler : AbillityHandler
    {
        [SerializeField] protected ItemCollector itemCollector;
        [SerializeField] protected int itemSlot;

        private void Start()
        {
            itemCollector.OnDuplicated += OnDuplicateAbility;
        }

        protected override void FixedUpdate()
        {
            if (itemCollector.Items.Count <=  itemSlot) return;

            if (abillitySO == null)
            {
                Abillity = itemCollector.Items[itemSlot];
            }

            base.FixedUpdate();
        }
        protected override void ResetDefault()
        {
            base.ResetDefault();

            if (abillitySO == null && itemCollector.Items.Count > itemSlot) itemCollector.Items.Remove(itemCollector.Items[itemSlot]);
        }

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