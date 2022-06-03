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