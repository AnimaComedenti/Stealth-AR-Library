using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace BehaviorTree
{
    public class MoveToDestinationNode : Node
    {
        private NavMeshAgent agent;
        private List<Pose> destinationPoints;
        private Pose currentDestination;
        private float rotatingSpeed;
        private int counter = 0;
        private float maxTime = 6;
        private float timer = 0;



        private RotatingNode rotate;

        public MoveToDestinationNode(NavMeshAgent agent, List<Pose> destinationPoints, float rotatingSpeed)
        {
            this.agent = agent;
            this.destinationPoints = destinationPoints;
            currentDestination = destinationPoints[0];

            rotate = new RotatingNode(agent.transform, rotatingSpeed);
            rotate.SetInstandLookDirection(currentDestination.position);
        }

        public override NodeState Evaluate()
        {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(agent.transform.position, currentDestination.position);

            if (distance >= 0.02f)
            {
                agent.isStopped = false;
                agent.SetDestination(currentDestination.position);
                return NodeState.RUNNING;
            }
            else
            {
                agent.isStopped = true;
                if(timer >= maxTime)
                {
                    timer = 0;
                    SetDestinationPoint();
                    rotate.SetLookDirection(currentDestination.position);
                    return NodeState.SUCCESS;
                }
                return NodeState.RUNNING;
            }
        }

        private void SetDestinationPoint()
        {

            if (destinationPoints.Count == 1) return;
            foreach(Pose destination in destinationPoints)
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

