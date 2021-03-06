using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Bei dieser Klasse handelt es sich um eine abstracte F?higkeit Klasse die geerbt werden kann. 
     * Dieser Klassentyp wird f?r F?higkeiten sowie Items genutzt.
     * Um die Klasse ausf?hren zu k?nnen existiert ein AbillityHandler, welcher die Abklingszeit der jeweiligen F?higkeit regelt und somit ?ber eine Update-Methode verf?gt.
     * Um der Klasse Funktionalit?ten zu geben wie das Aktivieren der F?higkeit oder das sek?ndliche Updaten einer Methode, m?ssen hierf?r zus?tzlich die entsprechenden Interfaces implementiert werden.
     * 
     * abillityName: Name der F?higkeit
     * icon: Bild bzw. Icon der F?higkeit
     * description: Beschreibung der F?higkeit
     * cooldown: Abklingszeit der F?higkeit
     * damage: Schaden die, die F?hgikeit macht
     * isItem: Unterscheidung ob es sich bei der F?higkeit um eine Item handelt
     * activationCount: Angabe wie oft eine F?higkeit aktiviert werden kann ohne auf Abklingszeit zu gehen
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
        [SerializeField] protected bool isItem;

        //Aktivierungsanzahl der F?higkeit. Gibt an wie oft eine F?higkeit aktiviert werden kann.
        [SerializeField] protected int activationCount = 1;

        #region Getter & Setter
        public Sprite Icon => icon;
        public string Name => abillityName;
        public string Description => description;
        public float Cooldown => cooldown;
        public float Damage => damage;
        public bool IsItem => isItem;
        public int ActivationCount => activationCount;


        #endregion

    }
}

