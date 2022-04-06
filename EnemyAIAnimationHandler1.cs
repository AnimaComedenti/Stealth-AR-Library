using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.AI;

public class EnemyAIAnimationHandler
{
    private PhotonView pv;
    private NavMeshAgent agent;

    public EnemyAIAnimationHandler(PhotonView pv, NavMeshAgent agent)
    {
        this.pv = pv;
        this.agent = agent;
    }
}
