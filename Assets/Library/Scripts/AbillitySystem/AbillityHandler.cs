using System.Collections;
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

            if (Abillity == null)
            {
                return;
            }

            if (activationLeft <= 0)
            {
                abillitySO = null;
            }

            //Wenn die Fähigkeit aktiviert wurde, beginne mit dem runterzählen der Abklingszeit
            if (HasBeenActivated)
            {
                cnt -= Time.deltaTime;
                if (cnt <= 0)
                {
                    cnt = Abillity.Cooldown;
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

            //Falls es sich bei der Abillity um ein Item handelt, zähl die Aktivierung runter.
            if (Abillity.IsItem) activationLeft--;

            activatableAbillity.Activate();
            HasBeenActivated = true;
        }


        //Das Reseten der Werte zum Default
        protected virtual void ResetDefault()
        {
            if (Abillity != null)
            {
                cnt = Abillity.Cooldown;
                activationLeft = Abillity.ActivationCount;
                HasBeenActivated = false;

                //Casten in die möglichen Fähigkeitstypen.
                skillUpdateable = Abillity as IUpdateableAbillity;
                activatableAbillity = Abillity as IActivatableAbillity;
            }
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
                if(abillitySO != null && abillitySO == value)
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
