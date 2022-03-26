using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
using Photon.Pun;

public class EnemyAI : MonoBehaviourPun
{
    [SerializeField] private PhotonView pv;
    [Header("WalkingPath")]
    [SerializeField] private List<Transform> movePositionsPose;
    [Header("Base Stats")]
    [SerializeField] private float startinghealth;
    [SerializeField] private float speedToRotate;
    [SerializeField] private float speedToRotateIfSeen;
    [SerializeField] private float timeToSearch;
    [Header("Detection")]
    [SerializeField] private float shootingRange;
    [SerializeField] private float viewDistance;
    [SerializeField] private Light flashLigth;
    [SerializeField] private int playerLayer;

    private List<Vector3> movePositions = new List<Vector3>();
    private GameObject _player;
    private Vector3 _lastSeenPlayerPosition;
    private float _currentHealth;
    private NavMeshAgent agent;
    private Selector topNode;

    public PhotonView photonV
    {
        get { return pv; }
    }
    


    private void Start()
    {
        _currentHealth = startinghealth;
        if (movePositionsPose.Count == 0) return;
        foreach(Transform pose in movePositionsPose)
        {
            movePositions.Add(pose.position);
        }
        BuildBehaviour();
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void BuildBehaviour()
    {
        CurrentlyDamagedNode checkCurrentlyDamaged = new CurrentlyDamagedNode(_currentHealth, this);
        //SequenzNode
        //checkHearedNode
        CheckPlayerSeenNode checkPlayer = new CheckPlayerSeenNode(this, flashLigth, viewDistance, playerLayer);
        MoveToDestinationNode moveToDestination = new MoveToDestinationNode(agent,movePositions,speedToRotate);
        CheckShootingRangeNode shootingRange = new CheckShootingRangeNode(playerLayer, this);
        ShootingNode shooting = new ShootingNode(this, speedToRotateIfSeen);
        ChasePlayerNode chasePlayer = new ChasePlayerNode(agent,this,speedToRotate);
        MoveToLastSeenPlayerPointNode moveToPlayerLastPosition = new MoveToLastSeenPlayerPointNode(agent,this,speedToRotate);
        //CheckHearing
        CheckSearchingTimer searchingTimer = new CheckSearchingTimer(timeToSearch,this);
        RotatingNode turnWhileSearching = new RotatingNode(this.transform,speedToRotate);

        Invertor CurrentlySeenPlayer = new Invertor(checkPlayer);
        Invertor checkIfNotShootingRange = new Invertor(shootingRange);

        Sequenz move = new Sequenz(new List<Node> { checkCurrentlyDamaged, CurrentlySeenPlayer, moveToDestination });
        Sequenz shoot = new Sequenz(new List<Node> { checkPlayer, shootingRange, shooting });
        Sequenz chase = new Sequenz(new List<Node> { checkPlayer, checkIfNotShootingRange, chasePlayer });
        Sequenz search = new Sequenz(new List<Node> { CurrentlySeenPlayer, moveToPlayerLastPosition, turnWhileSearching, CurrentlySeenPlayer, searchingTimer });

        topNode = new Selector(new List<Node> {search, move, shoot, chase});

    }

    private void Update()
    {
        if (topNode == null) return;
        topNode.Evaluate();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Transform child = transform.GetChild(0);
        Gizmos.DrawRay(child.position, transform.forward * viewDistance);
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
        set { _currentHealth = Mathf.Clamp(value, 0, startinghealth); }
    }

    public List<Vector3> DefaultMovePositions
    {
        get { return movePositions; }
        private set { movePositions = value; }      
    }

    [PunRPC]
    public void AddMovePositions(Vector3[] positions)
    {
        Debug.Log("Fuck "+positions);
        List<Vector3> positionList = new List<Vector3>();
        foreach (Vector3 pose in positions) {
            positionList.Add(pose);
        }
        movePositions = positionList;
        BuildBehaviour();
    }

    public float GetShootingRange { get { return shootingRange; } }
    public float GetChasingRange { get { return viewDistance; } }
    #endregion
}