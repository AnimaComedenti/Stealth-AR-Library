using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace BehaviorTree
{
    public class MoveToDestinationNode : Node
    {
        private NavMeshAgent agent;
        private List<Vector3> destinationPoints;
        private Vector3 currentDestination;
        private float rotatingSpeed;
        private int counter = 0;
        private float maxTime = 6;
        private float timer = 0;



        private RotatingNode rotate;

        public MoveToDestinationNode(NavMeshAgent agent, List<Vector3> destinationPoints, float rotatingSpeed)
        {
            this.agent = agent;
            this.destinationPoints = destinationPoints;

            rotate = new RotatingNode(agent.transform, rotatingSpeed);
            if (destinationPoints.Count >= 0)
            {
                currentDestination = destinationPoints[0];
                rotate.SetInstandLookDirection(currentDestination);
            }

        }

        public override NodeState Evaluate()
        {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(agent.transform.position, currentDestination);
            Vector3 gotoDestination = new Vector3(currentDestination.x, agent.transform.position.y, currentDestination.z);
            float scaleX = agent.transform.localScale.x * 2.5f;
            if (distance > scaleX)
            {
                agent.isStopped = false;
                //Check if the current destination is reachable, if not try again with a position which is has the same y as the agent
                if (!agent.SetDestination(currentDestination)){
                    agent.SetDestination(gotoDestination);
                }
                return NodeState.RUNNING;
            }
            else
            {
                Debug.Log("stoped");
                agent.isStopped = true;
                if(timer >= maxTime)
                {
                    timer = 0;
                    SetDestinationPoint();
                    rotate.SetLookDirection(currentDestination);
                    return NodeState.SUCCESS;
                }
                return NodeState.RUNNING;
            }
        }

        private void SetDestinationPoint()
        {

            if (destinationPoints.Count <= 1) return;
            foreach(Vector3 destination in destinationPoints)
            {
                if(destination == currentDestination)
                {
                    counter++;
                    if (counter > destinationPoints.Count-1) counter = 0;
                }
            }
 
            currentDestination = destinationPoints[counter];
        }
    }
}

