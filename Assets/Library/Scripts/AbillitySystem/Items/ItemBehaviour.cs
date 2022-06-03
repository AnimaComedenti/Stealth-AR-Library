using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Eine Klasse welche als Kontainer der F�higkeit dient.
     * In dieser Klasse wird die F�higkeit die f�r das Item genutzt wird angelegt und anhand der GetAbillity-Methode von au�en geholt.
     * Zudem ist diese Klasse f�r das zerst�ren bsw. Despawnen des Items zust�ndig. 
     * Hierbei wird das Gameobject auf dass, das Script liegt, entweder durch das Auslaufen einer Zeit zerst�rt.
     * 
     * abillity: Die Abillity welche das Item enth�lt.
     * timeToDespawn: Die Zeit (Lebenszeit des Items), ab wann das Item zerst�rt werden soll, falls es nicht aufgehoben wird
     * 
     */
    public class ItemBehaviour : MonoBehaviour
    {
        [SerializeField] private AbillitySO abillity;
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
        #region Getter & Setter
        public float TimeToDespawn { get => timeToDespawn; set => timeToDespawn = value; }

        public AbillitySO Abillity
        {
            get { 
                return abillity;
            }

            private set {
                abillity = value;
            }

        }
        #endregion
    }
}

