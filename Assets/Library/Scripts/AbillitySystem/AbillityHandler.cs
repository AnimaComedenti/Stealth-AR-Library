using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Der AbillitieHandler wird für das verarbeiten der Fähigkeiten benötigt.
     * Diese Klasse nimmt eine AbillitySO entgegen und verarbeitet dessen Abklingszeit und Update-Methoden.
     * In dieser Klasse kann die Fähigkeit auch null sein und hat somit den Vorteil diese per Runtime hinzufügen und entfernen zu können
     */
    public class AbillityHandler : MonoBehaviour
    {
        //Fähigkeit die Verarbeitet werden soll
        [SerializeField] protected AbillitySO abillitySO;

        // Falls eine Fähigkeit über eine Update- oder Aktivierungsmethoden verfügen soll, so muss dieses Interface implementiert werden.
        protected IUpdateableAbillity skillUpdateable;
        protected IActivatableAbillity activatableAbillity;

        protected float cnt = 0;

        private void Start()
        {
            ResetDefault();
        }

        protected virtual void FixedUpdate()
        {
            if (abillitySO == null)
            {
                return;
            }

            //Wenn die Fähigkeit aktiviert wurde, beginne mit dem ru7nterzählen der Abklingszeit
            if (HasBeenActivated)
            {
                cnt -= Time.deltaTime;
                if (cnt <= 0)
                {
                    cnt = abillitySO.Cooldown;
                    HasBeenActivated = false;
                }
                Cooldown = (int)cnt;
            }

            //Falls die Abillitie über eine Update-Methode verfügt, führe sie aus
            if (skillUpdateable != null) skillUpdateable.SkillUpdate();
        }

        public void OnActivate()
        {
            //Falls die Abklingszeit noch läuft und die Fähigkeit nochmal aktiviert wurde, return
            if (HasBeenActivated || activatableAbillity == null) return;

            activatableAbillity.Activate();
            HasBeenActivated = true;
        }


        //Das Reseten der Werte zum Default
        private void ResetDefault()
        {
            if (abillitySO != null)
            {
                cnt = 0;
                HasBeenActivated = false;

                //Casten in die möglichen Fähigkeitstypen.
                skillUpdateable = abillitySO as IUpdateableAbillity;
                activatableAbillity = abillitySO as IActivatableAbillity;
            }
        }

        /*
         * Abfrage für Externe Scripts ob die Fähigkeit von dieser Klasse in Laufzeit entfert werden kann.
         * Hierbei wird geprüft ob die maximale Aktivierungen überschritten worden sind und gibt true zurück fall der Wert unter dem default Wert liegt.
         */
        public bool CanBeRemoved()
        {
            if (abillitySO == null) return false;

            return abillitySO.ActivationCount < 1;
        }

        #region Getter & Setter

        //Public int welcher von außen genutzt werden kann
        public int Cooldown { get; private set; }

        //Bool zum überprüfen ob die Fähigkeit bereits gewirkt wurde
        public bool HasBeenActivated { get; private set; } = false;
        public AbillitySO Abillity
        {
            get { return abillitySO; }
            set
            {
                abillitySO = value;
                ResetDefault();
            }
        }
        #endregion
    }

}
