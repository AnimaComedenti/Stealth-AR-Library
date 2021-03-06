using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StealthLib.BehaviourNodes;

namespace StealthDemo.Nodes
{
    public class ChasePlayerNode : Node
    {
        private NavMeshAgent agent;
        private Transform player;
        private EnemyAI origin;

        private RotatingEnemyClass rotate;

        public ChasePlayerNode(NavMeshAgent agent, EnemyAI origin, float rotatingSpeed)
        {
            this.agent = agent;
            this.origin = origin;

            rotate = new RotatingEnemyClass(agent.transform,rotatingSpeed);

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
            if(distance == origin.GetShootingRange)
            {
                agent.isStopped = true;
                return NodeState.FAILURE;
            }
            if (distance <= origin.GetChasingRange && distance > origin.GetShootingRange)
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

