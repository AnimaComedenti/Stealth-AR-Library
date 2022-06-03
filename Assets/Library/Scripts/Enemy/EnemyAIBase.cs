using Photon.Pun;
using StealthLib.BehaviourNodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Basisklasse für die Gegner.
     * Hauptgedanke dieser Klasse ist es, diese als Dependencies-Injector-Klasse zu benutzen.
     * Diese Klasse nimmt die Dependecies entgegen und erstellt anahnd diesen ein BehviorTree.
     * 
     * health: Leben des Gegners
     * movementSpeed: Bewegungsgeschwindigkeit des Gegners
     * movePositionsPose: Die Positionen die der Gegner ablaufen kann
     */
    public abstract class EnemyAIBase : MonoBehaviourPun
    {
        [SerializeField] protected float health;
        [SerializeField] protected float movementSpeed;
        [Header("WalkingPath")]
        [SerializeField] protected List<Transform> movePositionsPose;

        protected Node topNode;
        protected List<Vector3> movePositions = new List<Vector3>();

        protected virtual void Start()
        {
            topNode = BuildTopNode();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            topNode.Evaluate();
        }

        /*
         * Methode welche den BehaviourTree erzeugt
         */
        protected abstract Node BuildTopNode();

        public List<Vector3> DefaultMovePositions
        {
            get { return movePositions; }
            private set { movePositions = value; }
        }

        [PunRPC]
        public void AddMovePositions(Vector3[] positions)
        {
            List<Vector3> positionList = new List<Vector3>();
            foreach (Vector3 pose in positions)
            {
                positionList.Add(pose);
            }
            movePositions = positionList;
            BuildTopNode();
        }
    }
}