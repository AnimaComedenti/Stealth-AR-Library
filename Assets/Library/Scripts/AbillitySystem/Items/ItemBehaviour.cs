using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Eine Klasse welche als Kontainer der F�higkeit dient.
     * In dieser Klasse wird die F�higkeit die f�r das Item genutzt wird angelegt und anhand der GetAbillity-Methode von au�en geholt.
     * Zudem ist diese Klasse f�r das Despawnen des Items zust�ndig. 
     * Hierbei wird das Gameobject auf dass, das Script liegt, entweder durch das Auslaufen einer Zeit oder wenn die GetAbillity-Methode aufgerufen wurde zerst�rt.
     */
    public class ItemBehaviour : MonoBehaviour
    {
        [SerializeField] private AbillitySO abillity;

        [SerializeField] private float timeToDespawn = 20;

        private bool hasItemTaken = false;
        private float cnt = 0;

        // Update is called once per frame
        void FixedUpdate()
        {

            if (hasItemTaken) Destroy(gameObject);

            cnt += Time.deltaTime;
            if (cnt == timeToDespawn)
            {
                Destroy(gameObject);
            }

        }
        public AbillitySO GetAbillity()
        {
            hasItemTaken = true;
            return abillity;
        }
    }
}

