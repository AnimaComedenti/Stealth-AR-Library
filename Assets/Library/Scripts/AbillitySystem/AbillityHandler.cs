﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Der AbillitieHandler wird für das verarbeiten der Fähigkeiten benötigt.
     * Diese Klasse nimmt eine AbillitySO entgegen und verarbeitet dessen Abklingszeiten, Update-Methoden sowie Aktivierungsfunktionen.
     * Zudem bestimtm diese Klasse ob eine Abillity entfernt werden kann
     * 
     * abillitySO: Die abillity welche verarbeitet werden soll.
     */
    public class AbillityHandler : MonoBehaviour
    {
        //Fähigkeit die Verarbeitet werden soll
        [SerializeField] protected AbillitySO abillitySO;

        // Falls eine Fähigkeit über eine Update- oder Aktivierungsmethoden verfügen soll, so muss dieses Interface implementiert werden.
        protected IUpdateableAbillity skillUpdateable;
        protected IActivatableAbillity activatableAbillity;
        protected int activationLeft = 1;
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

            if(activationLeft <= 0)
            {
                abillitySO = null;
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

            //Falls es sich bei der Abillity um ein Item handelt, zähl deren Aktivierung runter.
            if (abillitySO.IsItem) activationLeft--;

            activatableAbillity.Activate();
            HasBeenActivated = true;
        }


        //Das Reseten der Werte zum Default
        protected virtual void ResetDefault()
        {
            if (abillitySO != null)
            {
                cnt = abillitySO.Cooldown;
                activationLeft = abillitySO.ActivationCount;
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
            return activationLeft <= 0;
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
                if(abillitySO == value)
                {
                    activationLeft++;
                    return;
                }

                abillitySO = value;
                ResetDefault();
            }
        }
        #endregion
    }

}
