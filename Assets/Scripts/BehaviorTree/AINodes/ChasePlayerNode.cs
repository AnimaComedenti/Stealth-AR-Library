using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class ChasePlayerNode : Node
    {
        private NavMeshAgent agent;
        private Transform player;
        private EnemyAI origin;

        private RotatingNode rotate;

        public ChasePlayerNode(NavMeshAgent agent, EnemyAI origin, float rotatingSpeed)
        {
            this.agent = agent;
            this.origin = origin;

            rotate = new RotatingNode(agent.transform,rotatingSpeed);

        }

        public override NodeState Evaluate()
        {

            if (origin.Player == null)
            {
                Debug.LogError("Player not found");
                return NodeState.FAILURE;
            }

            player = origin.Player.transform;
            float distance = Vector3.Distance(origin.transform.position,player.position);

            if(distance <= origin.GetChasingRange && distance > origin.GetShootingRange)
            {
                agent.isStopped = false;
                rotate.SetLookDirection(player.transform.position);
                agent.SetDestination(player.transform.position / 2);
                return NodeState.RUNNING;
            }
            else
            {
                agent.isStopped = true;
                return NodeState.FAILURE;
            }
        }
    }
}

