using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthDemo.Nodes
{
    public class CheckPlayerSeenNode : Node
    {
        private List<GameObject> players;
        private EnemyAI origin;
        private Light lightResource;
        private float seeDistance;
        private float viewAngle;
        private string playerTag;

        public CheckPlayerSeenNode(EnemyAI origin, Light lightResource, float seeDistance, string playerTag)
        {
            this.lightResource = lightResource;
            this.seeDistance = seeDistance;
            this.origin = origin;
            this.playerTag = playerTag;
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
                if (player.gameObject.layer == 11) continue;

                if (Vector3.Distance(origin.transform.position, player.transform.position) < seeDistance)
                {
                    Transform child = origin.transform.GetChild(0);
                    Vector3 dirToPlayer = (player.transform.position - origin.transform.position).normalized;
                    float anglePlayerAndEnemy = Vector3.Angle(origin.transform.forward, dirToPlayer);
                    if(anglePlayerAndEnemy < viewAngle / 2f)
                    {
                        RaycastHit hit;
                        if(Physics.Linecast(child.position, player.transform.position, out hit))
                        {
                            if (hit.collider.gameObject.CompareTag(playerTag))
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
                if (player.gameObject.CompareTag(playerTag)) players.Add(player.gameObject);
            }
        }
    }
}

