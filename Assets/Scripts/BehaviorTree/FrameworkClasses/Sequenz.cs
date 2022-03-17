using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequenz : Node
    {

        protected List<Node> childNodes = new List<Node>();

        public Sequenz(List<Node> childNodes)
        {
            this.childNodes = childNodes;
        }
        public override NodeState Evaluate()
        {
            bool isAnyNodeRunning = false;
            foreach (Node node in childNodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        isAnyNodeRunning = true;
                        break;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.FAILURE:
                        _state = NodeState.FAILURE;
                        return _state;
                    default:
                        break;
                }
            }
            _state = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return _state;
        }
    }
}

