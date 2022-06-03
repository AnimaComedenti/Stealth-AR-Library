using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthLib;
using StealthLib.BehaviourNodes;

namespace StealthDemo.Nodes
{
    public class CheckSomthingHeardNode : Node
    {
        private EnemyAI origin;
        private SoundDetector soundDetector;
        private GameObject hearedObject;

        public CheckSomthingHeardNode(EnemyAI origin, SoundDetector soundDetector)
        {
            this.origin = origin;
            this.soundDetector = soundDetector;
        }

        public override NodeState Evaluate()
        {
            hearedObject = GetNearesHearedObject();
            if (hearedObject != null)
            {
                origin.LastSeenPlayerPosition = hearedObject.transform.position;
                return NodeState.FAILURE;
            }
            else
            {
                origin.Player = null;
                return NodeState.SUCCESS;
            }
        }

        private GameObject GetNearesHearedObject()
        {
            float nearesDistance = 0;
            float currentDistance = 0;

            GameObject nearestObject = null;

            foreach (GameObject hearedObject in soundDetector.CurrentHearingObjects)
            {
                currentDistance = Vector3.Distance(origin.transform.position, hearedObject.transform.position);

                if (hearedObject.Equals(soundDetector.CurrentHearingObjects[0])){
                    nearesDistance = currentDistance;
                    nearestObject = hearedObject;
                    continue;
                }

                if(currentDistance < nearesDistance)
                {
                    nearesDistance = currentDistance;
                    nearestObject = hearedObject;
                }
            }

            return nearestObject;
        }
    }
}
