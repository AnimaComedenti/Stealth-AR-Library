using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class RotatingEnemyClass {

        private Transform origin;
        private float rotatingSpeed;

        public RotatingEnemyClass(Transform origin, float rotatingSpeed)
        {
            this.origin = origin;
            this.rotatingSpeed = rotatingSpeed;

        }

        public void SetLookDirection(Vector3 destination)
        {
            if (destination != null)
            {
                Vector3 direction = (destination - origin.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                origin.transform.rotation = Quaternion.Slerp(origin.transform.rotation, lookRotation, Time.deltaTime * rotatingSpeed);
            }
        }

        public void SetInstandLookDirection(Vector3 destination)
        {
            if (destination != null)
            {
                Vector3 direction = (destination - origin.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                origin.transform.rotation = lookRotation;
            }
        }
    }
}

