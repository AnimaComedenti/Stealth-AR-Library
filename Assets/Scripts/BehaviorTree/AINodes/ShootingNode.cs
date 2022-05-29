using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthLib;

namespace StealthDemo.Nodes
{
    public class ShootingNode : Node
    {
        private EnemyAI origin;
        private AbillityHandler shootingHandler;
        private Transform player;

        private RotatingEnemyClass rotate;

        public ShootingNode(EnemyAI origin, AbillityHandler shootingHandler, float rotatingSpeed)
        {
            this.origin = origin;
            this.shootingHandler = shootingHandler;
            rotate = new RotatingEnemyClass(origin.transform, rotatingSpeed);
        }

        public override NodeState Evaluate()
        {
            if (origin.Player == null)
            {
                return NodeState.FAILURE;
            }
            player = origin.Player.transform;

            shootingHandler.OnActivate();
            rotate.SetLookDirection(player.position);
            return NodeState.SUCCESS;
        }
    }
}


