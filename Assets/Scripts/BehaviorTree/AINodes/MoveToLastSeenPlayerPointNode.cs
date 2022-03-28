using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class MoveToLastSeenPlayerPointNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAI origin;
        private Vector3 destinationPoint;


        private RotatingNode rotate;

        public MoveToLastSeenPlayerPointNode(NavMeshAgent agent, EnemyAI origin, float rotatingSpeed)
        {
            this.agent = agent;
            this.origin = origin;
            rotate = new RotatingNode(agent.transform, rotatingSpeed);
        }
        public override NodeState Evaluate()
        {
            destinationPoint = origin.LastSeenPlayerPosition;
            if (destinationPoint != Vector3.zero)
            {
                float distance = Vector3.Distance(agent.transform.position, destinationPoint);
                float scaleX = agent.transform.localScale.x * 2.5f;
                if (distance > scaleX)
                {
                    agent.isStopped = false;
                    rotate.SetLookDirection(destinationPoint);
                    agent.SetDestination(destinationPoint);
                    return NodeState.RUNNING;
                }
                else
                {
                    agent.isStopped = true;
                    return NodeState.SUCCESS;
                }
            }
            else
            {
                return NodeState.FAILURE;
            }

        }
    }
}

