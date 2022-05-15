using System.Collections;
using UnityEngine;

namespace StealthDemo
{
    public class ItemCollectBehavior : MonoBehaviour
    {
        [SerializeField] private ActivatableObject activatableItem;

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
        public ActivatableObject GetActivatable()
        {
            Destroy(gameObject);
            return activatableItem;
        }
    }
}