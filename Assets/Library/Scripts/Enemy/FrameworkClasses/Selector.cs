using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib.BehaviourNodes
{
    /*
     * Das Logische OR
     * Diese Klasse nimmt mehrere ChildNotes engegen, itteriert durch diese, und überprüft deren Status.
     * Die Note selber gibt, falls eine ChildNote Success zurück gibt, auch Success zurück.
     */
    public class Selector : Node
    {
        protected List<Node> childNodes = new List<Node>();
        public Selector(List<Node> childNodes)
        {
            this.childNodes = childNodes;
        }
        public override NodeState Evaluate()
        {
            foreach (Node node in childNodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return _state;
                    case NodeState.SUCCESS:
                        _state = NodeState.SUCCESS;
                        return _state;
                    case NodeState.FAILURE:
                        break;
                    default:
                        break;
                }
            }
            _state = NodeState.FAILURE;
            return _state;
        }
    }
}

