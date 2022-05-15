using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AbillitySO")]

    public class AbillitySO : ScriptableObject
    {
        [SerializeField] string name;
        [SerializeField] Sprite icon;
        [SerializeField] string description;
        [SerializeField] float cooldown;
        [SerializeField] float damage;

        public Sprite Icon => icon;
        public string Name => name;
        public string Description => description;
        public float Cooldown => cooldown;
        public float Damage => damage;
    }
}
