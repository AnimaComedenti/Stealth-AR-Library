using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StealthDemo
{
    [CreateAssetMenu(menuName ="ScriptableObjects/BuildableObjectsSO")]
    public class BuildableObjectSO : ScriptableObject
    {
        [SerializeField] private Transform prefab;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string buildingName;

        public Transform Prefab => prefab;
        public Sprite Sprite => sprite;
        public string Name => buildingName;
    }
}

