using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class RotatingNode: Node {

        private Transform origin;
        private float rotatingSpeed;
        private Transform originalRotation;
        private float rotation = 0;
        private bool hasBeenRotated = false;
        float currenttime = 0;

        public RotatingNode(Transform origin, float rotatingSpeed)
        {
            this.origin = origin;
            this.rotatingSpeed = rotatingSpeed;
            originalRotation = origin;
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

        public override NodeState Evaluate()
        {
            return RotateAround();
        }

        public NodeState RotateAround()
        {

            if (!hasBeenRotated)
            {
                rotation += Time.deltaTime * 0.5f;
            }
            else
            { 
                rotation -= Time.deltaTime * 0.5f;
            }
            if(rotation >= 0.5)
            {
                hasBeenRotated = true;
            }else if (rotation <= -0.5)
            {
                hasBeenRotated = false;
            }

           origin.transform.Rotate(0f, rotation, 0f);
           return NodeState.SUCCESS;
        }
    }
}

