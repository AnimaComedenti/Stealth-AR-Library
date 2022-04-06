using UnityEngine;
using System.Collections;
using BehaviorTree;
using Photon.Pun;
using UnityEngine.AI;

public class RotateNode : Node
{
    private Transform origin;
    private float rotation = 0;
    private bool hasBeenRotated = false;

    public RotateNode(Transform origin)
    {
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        return RotateAround();
    }

    public NodeState RotateAround()
    {

        if (!hasBeenRotated)
        {
            rotation += Time.deltaTime * 0.5f;
        }
        else
        {
            rotation -= Time.deltaTime * 0.5f;
        }
        if (rotation >= 0.5)
        {
            hasBeenRotated = true;
        }
        else if (rotation <= -0.5)
        {
            hasBeenRotated = false;
        }

        origin.transform.Rotate(0f, rotation, 0f);
        return NodeState.SUCCESS;
    }
}
