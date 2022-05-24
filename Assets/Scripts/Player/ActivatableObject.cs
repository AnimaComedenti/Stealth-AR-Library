using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;

namespace StealthDemo
{
    public class ActivatableObject : MonoBehaviour
    {
        [SerializeField] AbillitySO abillitySO;
        public AbillitySO abillity
        {
            get { return abillitySO; }
            set {
                abillitySO = value;
                SetStartMethods();
            }
        }
        public float cooldownText;

        private ISkillUpdateable skillUpdateable;

        private float cnt = 0;

        private void Start()
        {
             SetStartMethods();
        }

        private void FixedUpdate()
        {
            if (abillitySO == null) return;

            if (abillitySO.HasBeenActivated)
            {
                cnt -= Time.deltaTime;
                if (cnt <= 0)
                {
                    cnt = abillity.Cooldown;
                    abillity.HasBeenActivated = false;
                }

                cooldownText= (int)cnt;
            }

            if (skillUpdateable != null) skillUpdateable.OnUpdating();
        }

        public Sprite GetButtonSprite()
        {
            return abillitySO.Icon;
        }

        public bool CanBeRemoved()
        {
            if (abillitySO == null) return false;

            return abillitySO.ActivationCount <= 0;
            
        }
        public  void OnActivate() {

            abillitySO.OnSkillActivation();
        }


        private void SetStartMethods()
        {
            if (abillitySO != null)
            {
                cnt = 0;
                skillUpdateable = abillitySO as ISkillUpdateable;
            }
        }
    }
}
