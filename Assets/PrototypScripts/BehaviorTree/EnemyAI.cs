using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using  StealthDemo.Nodes;
using Photon.Pun;
using StealthLib;
using StealthLib.BehaviourNodes;

namespace StealthDemo
{
    public class EnemyAI : EnemyAIBase
    {
        [SerializeField] private EnemyShootingHandler shootingHandler;
        [Header("Base Stats")]
        [SerializeField] private float speedToRotate;
        [SerializeField] private float speedToRotateIfSeen;
        [SerializeField] private float timeToSearch;
        [Header("Detection")]
        [SerializeField] private float shootingRange;
        [SerializeField] private float viewDistance;
        [SerializeField] private Light flashLigth;
        [SerializeField] private string playerTag = "Player";
        [Header("SoundDetection")]
        [SerializeField] private SoundDetector soundDetector;

        private GameObject _player;
        private Vector3 _lastSeenPlayerPosition;
        private float _currentHealth;
        private NavMeshAgent agent;

        protected override void Start()
        {
            _currentHealth = health;
            if (movePositionsPose.Count == 0) return;
            foreach (Transform pose in movePositionsPose)
            {
                movePositions.Add(pose.position);
            }
            base.Start();
        }
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        protected override Node BuildTopNode()
        {
            CurrentlyDamagedNode checkCurrentlyDamaged = new CurrentlyDamagedNode(_currentHealth, this);

            //SequenzNode
            CheckSomthingHeardNode somethingHeared = new CheckSomthingHeardNode(this, soundDetector);
            CheckPlayerSeenNode checkPlayer = new CheckPlayerSeenNode(this, flashLigth, viewDistance, playerTag);
            MoveToDestinationNode moveToDestination = new MoveToDestinationNode(agent, movePositions, speedToRotate);
            CheckShootingRangeNode shootingRange = new CheckShootingRangeNode(this);
            ShootingNode shooting = new ShootingNode(this, shootingHandler, speedToRotateIfSeen);
            ChasePlayerNode chasePlayer = new ChasePlayerNode(agent, this, speedToRotate);
            MoveToLastSeenPlayerPointNode moveToPlayerLastPosition = new MoveToLastSeenPlayerPointNode(agent, this, speedToRotate);

            //CheckHearing
            CheckSearchingTimer searchingTimer = new CheckSearchingTimer(timeToSearch, this);
            RotateNode turnWhileSearching = new RotateNode(this.transform);

            Invertor CurrentlySeenPlayer = new Invertor(checkPlayer);
            Invertor checkIfNotShootingRange = new Invertor(shootingRange);

            Sequenz movingToHearLocation = new Sequenz(new List<Node> { moveToPlayerLastPosition, CurrentlySeenPlayer, turnWhileSearching });

            Selector checkHearing = new Selector(new List<Node> { somethingHeared, movingToHearLocation });

            Sequenz move = new Sequenz(new List<Node> { checkCurrentlyDamaged, CurrentlySeenPlayer, checkHearing, moveToDestination });
            Sequenz shoot = new Sequenz(new List<Node> { checkPlayer, shootingRange, shooting });
            Sequenz chase = new Sequenz(new List<Node> { checkPlayer, checkIfNotShootingRange, chasePlayer });
            Sequenz search = new Sequenz(new List<Node> { CurrentlySeenPlayer, moveToPlayerLastPosition, turnWhileSearching, checkHearing, CurrentlySeenPlayer, searchingTimer });

            topNode = new Selector(new List<Node> { search, move, shoot, chase });

            return topNode;
        }

        protected override void Update()
        {
            if (topNode == null) return;
            base.Update();
        }


        #region Getter&Setter

        public GameObject Player
        {
            set { _player = value; }
            get { return _player; }
        }

        public Vector3 LastSeenPlayerPosition
        {
            set { _lastSeenPlayerPosition = value; }
            get { return _lastSeenPlayerPosition; }
        }

        public float CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, health); }
        }


        public float GetShootingRange { get { return shootingRange; } }
        public float GetChasingRange { get { return viewDistance; } }
        #endregion
    }
}
