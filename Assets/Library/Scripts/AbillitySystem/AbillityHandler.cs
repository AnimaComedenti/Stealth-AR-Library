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
        [SerializeField] private AbillitySO abillitySO;

        //Public int welcher von außen genutzt werden kann
        public int CooldownText { get; private set; }

        // Falls eine Fähigkeit über eine Update- oder Aktivierungsmethoden verfügen soll, so muss dieses Interface implementiert werden.
        private IUpdateableAbillity skillUpdateable;
        private IActivatableAbillity activatableAbillity;

        private float cnt = 0;

        //Bool zum überprüfen ob die Fähigkeit bereits gewirkt wurde
        private bool hasBeenActivated = false;

        private void Start()
        {
            ResetDefault();
        }

        private void FixedUpdate()
        {
            if (abillitySO == null) return;

            //Wenn die Fähigkeit aktiviert wurde, beginne mit dem ru7nterzählen der Abklingszeit
            if (hasBeenActivated)
            {
                cnt -= Time.deltaTime;
                if (cnt <= 0)
                {
                    cnt = abillitySO.Cooldown;
                    hasBeenActivated = false;
                }
                CooldownText = (int)cnt;
            }

            //Falls die Abillitie über eine Update-Methode verfügt, führe sie aus
            if (skillUpdateable != null) skillUpdateable.Update();
        }

        public void OnActivate()
        {
            //Falls die Abklingszeit noch läuft und die Fähigkeit nochmal aktiviert wurde, return
            if (hasBeenActivated || activatableAbillity == null) return;

            activatableAbillity.Activate();
            hasBeenActivated = true;
        }


        //Das Reseten der Werte zum Default
        private void ResetDefault()
        {
            if (abillitySO != null)
            {
                cnt = 0;
                hasBeenActivated = false;

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

        public AbillitySO Abillity
        {
            get { return abillitySO; }
            set
            {
                abillitySO = value;
                ResetDefault();
            }
        }
    }

}
