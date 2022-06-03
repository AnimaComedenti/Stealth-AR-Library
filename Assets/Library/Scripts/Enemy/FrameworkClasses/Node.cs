using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib.BehaviourNodes
{
    /*
     * Basisklasse f�r die BehaviourTree-Logik.
     * Diese Notes werde als Logischegatter genutzt und geben einen Status anhand der Evalute-Methode zur�ck.
     * Anhand dieser Logik lassen sich BehaviorTress aufstellen welche komplexe abl�ufe erm�glichen.
     * 
     * Success == true
     * Failure == false
     */
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

