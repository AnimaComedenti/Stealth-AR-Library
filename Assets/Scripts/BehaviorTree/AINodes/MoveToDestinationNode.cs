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

            if (distance >= 1f)
            {
                agent.isStopped = false;
                Vector3 destination = new Vector3(currentDestination.x, agent.transform.position.y, currentDestination.z);
                agent.SetDestination(destination);
                return NodeState.RUNNING;
            }
            else
            {
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

