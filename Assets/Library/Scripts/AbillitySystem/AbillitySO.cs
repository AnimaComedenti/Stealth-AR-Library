using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Bei dieser Klasse handelt es sich um eine abstracte Fähigkeit Klasse die geerbt werden kann. 
     * Dieser Klassentyp wird für Fähigkeiten sowie Items genutzt.
     * Um die Klasse ausführen zu können existiert ein AbillityHandler, welcher die Abklingszeit der jeweiligen Fähigkeit regelt und somit über eine Update-Methode verfügt.
     * Um der Klasse Funktionalitäten zu geben wie das Aktivieren der Fähigkeit oder das sekündliche Updaten einer Methode, müssen hierfür zusätzlich die entsprechenden Interfaces implementiert werden.
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

        //Aktivierungsanzahl der Fähigkeit. Gibt an wie oft eine Fähigkeit aktiviert werden kann.
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

