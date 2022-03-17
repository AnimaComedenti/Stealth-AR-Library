using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckSearchingTimer : Node
    {
        private float timeToSearch;
        private float currentTime = 0;
        private EnemyAI origin;

        public CheckSearchingTimer (float timeToSearch, EnemyAI origin)
        {
            this.timeToSearch = timeToSearch;
            this.origin = origin;
        }

        public override NodeState Evaluate()
        {
            if(currentTime >= timeToSearch)
            {
                currentTime = 0;
                origin.LastSeenPlayerPosition = Vector3.zero;
                return NodeState.FAILURE;
            }
            else
            {
                currentTime += Time.deltaTime;
                return NodeState.SUCCESS;
            }
        }

    }
}

