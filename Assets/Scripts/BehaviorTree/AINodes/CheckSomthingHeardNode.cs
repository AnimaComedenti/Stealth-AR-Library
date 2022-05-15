using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            hearedObject = soundDetector.CurrentHearingObject;
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
    }
}
