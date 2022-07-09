using System.Collections;
using UnityEngine;

namespace StealthLib
{
    /*
     * Diese Klasse erbt von der Skillbutton Klassen und ist für das Zuweisen des PC-Spielers zuständig.
     * Diese Klasse nimmt ein beliebiges Taste und Mapt diese zu einem Input.
     * 
     * Key: Die Taste welche für die Aktivierung gedrückt werden soll
     */
    public class HiderSkillButton : SkillButtonWithCooldownText
    {

        [SerializeField] private KeyCode key;

        protected override void FixedUpdate()
        {

            if (Input.GetKey(key))
            {
                if (skillToActivate == null) return;
                abillityHandler.OnActivate();
            }

            base.FixedUpdate();
        }

        #region Getter & Setter
        public KeyCode Key { get => key; set => key = value; }
        #endregion
    }
}