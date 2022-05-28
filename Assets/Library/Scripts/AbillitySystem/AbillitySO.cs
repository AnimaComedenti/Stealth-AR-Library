using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Bei dieser Klasse handelt es sich um eine abstracte F�higkeit Klasse die geerbt werden kann. 
     * Dieser Klassentyp wird f�r F�higkeiten sowie Items genutzt.
     * Um die Klasse ausf�hren zu k�nnen existiert ein AbillityHandler, welcher die Abklingszeit der jeweiligen F�higkeit regelt und somit �ber eine Update-Methode verf�gt.
     * Um der Klasse Funktionalit�ten zu geben wie das Aktivieren der F�higkeit oder das sek�ndliche Updaten einer Methode, m�ssen hierf�r zus�tzlich die entsprechenden Interfaces implementiert werden.
     */
    public abstract class AbillitySO : ScriptableObject
    {

        [Header("Abillity Visualisierung")]
        [SerializeField] protected string abillityName;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string description;

        [Header("Abillity Defaultwerte")]
        [SerializeField] protected float cooldown;
        [SerializeField] protected float damage;

        //Aktivierungsanzahl der F�higkeit. Gibt an wie oft eine F�higkeit aktiviert werden kann.
        [SerializeField] protected int activationCount = 1;

        #region Getter & Setter
        public Sprite Icon => icon;
        public string Name => abillityName;
        public string Description => description;
        public float Cooldown => cooldown;
        public float Damage => damage;

        public int ActivationCount
        {
            get { return activationCount; }
            set { activationCount = value; }
        }

        #endregion

    }
}

