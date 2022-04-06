using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class ShootingNode : Node
    {
        private EnemyAI origin;
        private float rotatingSpeed;
        private Transform player;

        private RotatingEnemyClass rotate;

        public ShootingNode(EnemyAI origin, float rotatingSpeed)
        {
            this.origin = origin;
            this.rotatingSpeed = rotatingSpeed;
            rotate = new RotatingEnemyClass(origin.transform, rotatingSpeed);
        }

        public override NodeState Evaluate()
        {
            if (origin.Player == null)
            {
                Debug.LogError("Player not found");
                return NodeState.FAILURE;
            }
            player = origin.Player.transform;

            Debug.Log("Shoot");
            rotate.SetLookDirection(player.position);
            return NodeState.SUCCESS;
        }
    }
}

