using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Invertor : Node
    {

        protected Node child;

        public Invertor(Node child)
        {
            this.child = child;
        }
        public override NodeState Evaluate()
        {

            switch (child.Evaluate())
            {
                case NodeState.RUNNING:
                    _state = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    _state = NodeState.FAILURE;
                    break;
                case NodeState.FAILURE:
                    _state = NodeState.SUCCESS;
                    break;
                default:
                    break;
            }
            return _state;
        }
    }
}

