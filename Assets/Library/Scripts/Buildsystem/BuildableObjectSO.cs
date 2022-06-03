using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StealthLib
{
    /*
     * Container für die Objekte welche gebaut werden könne.
     * 
     * prefab: Das Objekt welches gebaut werden soll.
     * sprite: Das Bild dass, das Objekt enthält
     * buildingName: Der Name des Bauwerks
     * descriptions: Die Beschreibung des Bauwerks
     */
    [CreateAssetMenu(menuName ="ScriptableObjects/BuildableObjectsSO")]
    public class BuildableObjectSO : ScriptableObject
    {
        [SerializeField] private Transform prefab;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string buildingName;
        [SerializeField] private string description;

        public Transform Prefab => prefab;
        public Sprite Sprite => sprite;
        public string Name => buildingName;
        public string Description => description;
    }
}

