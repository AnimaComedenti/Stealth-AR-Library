using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckShootingRangeNode : Node
    {
        private LayerMask playerLayer;
        private EnemyAI origin;
        private Transform player;


        public CheckShootingRangeNode(LayerMask playerLayer, EnemyAI origin)
        {
            this.playerLayer = playerLayer;
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
                if (!Physics.Linecast(origin.transform.position, player.transform.position, playerLayer))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
