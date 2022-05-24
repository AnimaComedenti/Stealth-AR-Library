using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    public class ItemCollectBehavior : MonoBehaviour
    {
        [SerializeField] private AbillitySO activatableItem;

        [SerializeField] private float timeToDespawn = 20;

        private float cnt = 0;

        // Update is called once per frame
        void FixedUpdate()
        {
            cnt += Time.deltaTime;

            if (cnt == timeToDespawn)
            {
                Destroy(gameObject);
            }

        }
        public AbillitySO GetActivatable()
        {
            Invoke("DestroyObject", 0.5f);
            return activatableItem;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}