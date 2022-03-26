using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckPlayerSeenNode : Node
    {
        private List<GameObject> players;
        private EnemyAI origin;
        private Light lightResource;
        private int playerLayer;
        private float seeDistance;
        private float viewAngle;

        public CheckPlayerSeenNode(EnemyAI origin, Light lightResource, float seeDistance, int playerLayer)
        {
            this.lightResource = lightResource;
            this.seeDistance = seeDistance;
            this.origin = origin;
            this.playerLayer = playerLayer;
            viewAngle = lightResource.spotAngle;
        }

        public override NodeState Evaluate()
        {
            SetPlayer();
            return CanSeePlayer() ? NodeState.SUCCESS : NodeState.FAILURE;
        }

        public bool CanSeePlayer()
        {
            if (players.Count == 0) return false;
            foreach (GameObject player in players)
            {
                
                if (Vector3.Distance(origin.transform.position, player.transform.position) < seeDistance)
                {
                    Transform child = origin.transform.GetChild(0);
                    Vector3 dirToPlayer = (player.transform.position - origin.transform.position).normalized;
                    float anglePlayerAndEnemy = Vector3.Angle(origin.transform.forward, dirToPlayer);
                    if(anglePlayerAndEnemy < viewAngle / 2f)
                    {
                        RaycastHit hit;
                        Physics.Linecast(child.position, player.transform.position, out hit, 1 << playerLayer);
                        
                        if(hit.collider != null)
                        {
                            if (hit.collider.gameObject.CompareTag("Player"))
                            {
                                lightResource.color = Color.red;
                                origin.LastSeenPlayerPosition = player.transform.position;
                                origin.Player = player;
                                return true;
                            }
                        }    
                    }
                }
            }
            origin.Player = null;
            lightResource.color = Color.yellow;
            return false;
        }

        public void SetPlayer()
        {
            players = new List<GameObject>();
            Collider[] hitColliders = Physics.OverlapSphere(origin.transform.position, seeDistance);
            foreach (Collider player in hitColliders)
            {
                if (player.gameObject.layer == playerLayer) players.Add(player.gameObject);
            }
        }
    }
}
