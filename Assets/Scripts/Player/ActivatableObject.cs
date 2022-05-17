using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthDemo
{
    public abstract class ActivatableObject : MonoBehaviour
    {
        public AbillitySO abillity;
        public float cooldownText;

        protected float cnt = 0;
        public bool hasBeenActivated = false;

        private float timesToUse = 0;

        public float SetTimesToUse {  get { return timesToUse; }  set { timesToUse = value; } }


        private void Start()
        {
            cnt = abillity.Cooldown;
            timesToUse = abillity.ActivationCount;
            if (timesToUse == 0) timesToUse = -1;
        }

        protected virtual void FixedUpdate()
        {

            if (hasBeenActivated)
            {
                cnt -= Time.deltaTime;
                if (cnt <= 0)
                {
                    cnt = abillity.Cooldown;
                    hasBeenActivated = false;
                }

                cooldownText= (int)cnt;
            }
        }

        public  Sprite GetButtonSprite()
        {
            return abillity.Icon;
        }
        public abstract ActivatableObject OnActivate();
    }
}
