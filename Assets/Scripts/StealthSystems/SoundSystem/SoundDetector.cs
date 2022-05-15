using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthDemo
{
    public class SoundDetector : MonoBehaviour
    {
        [SerializeField] private float maxHearDistance;
        private float distance;
        private float volumePerDistance;
        private float objectVolume;

        public GameObject CurrentHearingObject { get; private set; }
        // Update is called once per frame
        void FixedUpdate()
        {
            doSphereCast();
        }

        private void doSphereCast()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {

                    //If its himself, return
                    if (collider.gameObject.CompareTag(gameObject.tag)) continue;

                    if (collider.gameObject.TryGetComponent(out SoundMaker soundMaker))
                    {
                        Vector3 makerPosition = soundMaker.transform.position;
                        distance = Vector3.Distance(makerPosition, transform.position);
                        if (soundMaker.Volume() == 0) continue;
                        volumePerDistance = 0.7f / distance;
                        objectVolume = soundMaker.Volume() + volumePerDistance;
                        if (objectVolume >= 1)
                        {
                            CurrentHearingObject = collider.gameObject;
                            return;
                        }
                    }
                }
            }
            CurrentHearingObject = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, maxHearDistance);
        }
    }
}
