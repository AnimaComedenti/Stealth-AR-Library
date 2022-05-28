using System.Collections;
using UnityEngine;

namespace StealthLib
{
    public class HiderSkillButton : SkillButton
    {

        [SerializeField] private KeyCode key;

        protected override void Update()
        {

            if (Input.GetKey(key))
            {
                abillityHandler.OnActivate();

                if (abillityHandler.CanBeRemoved())
                {
                    SetActivatableItem(null);
                    abillityHandler.Abillity = null;
                }
            }

            base.Update();
        }

        #region Getter & Setter
        public KeyCode Key { get => key; set => key = value; }
        #endregion
    }
}