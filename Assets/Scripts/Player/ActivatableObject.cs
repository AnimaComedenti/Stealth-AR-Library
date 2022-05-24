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

        private int timesToUse = 1;


        private void Start()
        {
             SetStartMethods();
        }

        private void FixedUpdate()
        {
            if (abillitySO == null) return;

            timesToUse = abillitySO.ActivationCount;


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
            return timesToUse <= 0;
        }
        public  void OnActivate() {

            abillitySO.OnSkillActivation();
        }

        private void SetStartMethods()
        {
            if (abillitySO != null)
            {
                cnt = abillitySO.Cooldown;
                skillUpdateable = abillitySO as ISkillUpdateable;
            }
        }
    }
}
