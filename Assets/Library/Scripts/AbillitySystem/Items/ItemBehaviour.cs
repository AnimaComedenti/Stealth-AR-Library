using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Eine Klasse welche als Kontainer der Fähigkeit dient.
     * In dieser Klasse wird die Fähigkeit die für das Item genutzt wird angelegt und anhand der GetAbillity-Methode von außen geholt.
     * Zudem ist diese Klasse für das zerstören bsw. Despawnen des Items zuständig. 
     * Hierbei wird das Gameobject auf dass, das Script liegt, entweder durch das Auslaufen einer Zeit zerstört.
     * 
     * abillity: Die Abillity welche das Item enthält.
     * timeToDespawn: Die Zeit (Lebenszeit des Items), ab wann das Item zerstört werden soll, falls es nicht aufgehoben wird
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

