using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StealthLib
{

    public abstract class  Abillity : ScriptableObject
    {
        [Header("AbillityDefault")]
        [SerializeField] protected string name;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string description;
        [SerializeField] protected float cooldown;
        [SerializeField] protected float damage;
        [SerializeField] protected int activationCount = 1;
        [SerializeField] protected bool hasbeenActivated;

        public int ActivationCount
        {
            get { return activationCount; }
            set {activationCount = value; }
        }

        public Sprite Icon => icon;
        public string Name => name;
        public string Description => description;
        public float Cooldown => cooldown;
        public float Damage => damage;

        public bool HasBeenActivated
        {
            get { return hasbeenActivated; }
            set { hasbeenActivated = value; }
        }


        public abstract void OnSkillActivation();

    }
}
