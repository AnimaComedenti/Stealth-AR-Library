using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthLib.BehaviourNodes;

namespace StealthDemo.Nodes
{
    public class CheckShootingRangeNode : Node
    {
        private EnemyAI origin;
        private Transform player;


        public CheckShootingRangeNode(EnemyAI origin)
        {
            this.origin = origin;
        }

        public override NodeState Evaluate()
        {
            if (origin.Player == null)
            {
                Debug.LogError("Player not found");
                return NodeState.FAILURE;
            }
            player = origin.Player.transform;
            return CanShootPlayer() ? NodeState.SUCCESS : NodeState.FAILURE;
        }

        private bool CanShootPlayer()
        {
            float distance = Vector3.Distance(origin.transform.position, player.position);
            if (distance <= origin.GetShootingRange)
            {
                if (Physics.Linecast(origin.transform.position, player.transform.position))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
