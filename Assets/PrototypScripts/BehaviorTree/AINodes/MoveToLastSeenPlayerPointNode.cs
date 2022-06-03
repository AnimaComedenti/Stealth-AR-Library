using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using StealthLib.BehaviourNodes;

namespace StealthDemo.Nodes
{
    public class MoveToLastSeenPlayerPointNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAI origin;
        private Vector3 destinationPoint;


        private RotatingEnemyClass rotate;

        public MoveToLastSeenPlayerPointNode(NavMeshAgent agent, EnemyAI origin, float rotatingSpeed)
        {
            this.agent = agent;
            this.origin = origin;
            rotate = new RotatingEnemyClass(agent.transform, rotatingSpeed);
        }
        public override NodeState Evaluate()
        {
            destinationPoint = origin.LastSeenPlayerPosition;
            if (destinationPoint != Vector3.zero)
            {
                agent.SetDestination(destinationPoint);
                if (agent.remainingDistance > 0.01f)
                {
                    agent.isStopped = false;
                    rotate.SetLookDirection(destinationPoint);
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

