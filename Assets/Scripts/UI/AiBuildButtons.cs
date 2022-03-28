using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;
using Photon.Pun;
using BehaviorTree;



public class AiBuildButtons : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] ARUIButtonScript arUihandler;
    private BuildableObjectSO buildableObject;
    public static List<GameObject> points = new List<GameObject>();
    public static List<Vector3> listWithPositions = new List<Vector3>();

    public BuildableObjectSO setBuildableObject
    {
        set { buildableObject = value; }
    }

    public void SetAiPositions()
    {
        Pose position = SeekerPlacementIndicator.Instance.getPlacementPosition;
        GameObject circle = Instantiate(pointPrefab, position.position, Quaternion.identity);
        listWithPositions.Add(position.position);
        points.Add(circle);
    }

    public void ResetLastPosition()
    {

        if (listWithPositions.Count <= 0)
        {
            Debug.Log("PositionList Empty");
            arUihandler.ToggelAiUIButtons();
        }
        else
        {
            GameObject lastPoint = points[points.Count - 1];
            Destroy(lastPoint);
            listWithPositions.RemoveAt(listWithPositions.Count - 1);
            points.RemoveAt(points.Count - 1);
        }
    }

    public void ConfirmAIBuild()
    {

        GameObject ai = SeekerPlacementIndicator.Instance.SpawnObject(buildableObject.prefab.gameObject, Quaternion.identity);
        EnemyAI aiScript = ai.GetComponent<EnemyAI>();
        Vector3[] vect3array = listWithPositions.ToArray();
        aiScript.photonV.RPC("AddMovePositions", RpcTarget.AllBuffered, vect3array);
        foreach (GameObject point in points)
        {
            Destroy(point);
        }

        listWithPositions.Clear();
        points.Clear();
        arUihandler.ToggelAiUIButtons();
    }

}
