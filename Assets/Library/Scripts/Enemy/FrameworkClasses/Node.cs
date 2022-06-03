using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib.BehaviourNodes
{
    public abstract class Node
    {
        protected NodeState _state ;
        public NodeState state { get { return _state; } }
        protected List<Node> children;

        public abstract NodeState Evaluate();
    }

    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }
}

