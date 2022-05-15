using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthDemo
{
    public abstract class ActivatableObject : MonoBehaviour
    {
        [SerializeField] protected AbillitySO abillity;
        [SerializeField] public float cooldownText;

        protected float cnt = 0;
        public bool hasBeenActivated = false;

        //if it is an ability get the count value
        protected ItemSO item;
        protected float timesToUse = 0;

        private void Start()
        {
            cnt = abillity.Cooldown;

            var item = (ItemSO)abillity;
            if (item != null)
            {
                this.item = item;
                timesToUse = item.ItemCount;
            }
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
