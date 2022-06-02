using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthDemo.Nodes
{
    public class CurrentlyDamagedNode : Node
    {
        private float savedHealth;
        private EnemyAI ai;

        public CurrentlyDamagedNode(float savedHealth, EnemyAI ai)
        {
            this.savedHealth = savedHealth;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            _state = savedHealth <= ai.CurrentHealth ? NodeState.SUCCESS : NodeState.FAILURE;
            savedHealth = ai.CurrentHealth;
            return _state;
        }
    }

}
