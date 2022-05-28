using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    public class ItemCollectBehavior : MonoBehaviour
    {
        [SerializeField] private Abillity activatableItem;

        [SerializeField] private float timeToDespawn = 20;

        private bool hasItemTaken = false;
        private float cnt = 0;

        // Update is called once per frame
        void FixedUpdate()
        {

            if(hasItemTaken) Destroy(gameObject); 

            cnt += Time.deltaTime;

            if (cnt == timeToDespawn)
            {
                Destroy(gameObject);
            }

        }
        public Abillity GetAbillity()
        {
            hasItemTaken = true;
            return activatableItem;
        }
    }
}