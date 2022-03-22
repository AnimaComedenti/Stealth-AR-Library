using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;
public class AiBuildButtons : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;

    private BuildableObjectSO buildableObject;
    private LineRenderer lineRenderer;
    private ARUIButtonScript buildButtons;
    private List<GameObject> points;

    public BuildableObjectSO setBuildableObject
    {
        set { buildableObject = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        buildButtons = FindObjectOfType<ARUIButtonScript>();
        lineRenderer = FindObjectOfType<LineRenderer>();
    }

    public void SetAiPositions()
    {
        GameObject ai = buildableObject.prefab.gameObject;
        EnemyAI aiScript = ai.GetComponent<EnemyAI>();
        Pose position = SeekerPlacementIndicator.Instance.getPlacementPosition;
        Debug.Log($"Set Position: {position}");
        aiScript.addDefaultMovePositionsEntry = position;
        GameObject circle = Instantiate(pointPrefab, position.position, Quaternion.identity);
        points.Add(circle);

        if (points.Count < 1)
        {
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i].transform.position);
            }
        }

    }

    public void ResetLastPosition()
    {
        GameObject ai = buildableObject.prefab.gameObject;
        EnemyAI aiScript = ai.GetComponent<EnemyAI>();
        List<Pose> positionList = aiScript.DefaultMovePositions;

        if (positionList.Count <= 0)
        {
            Debug.Log("PositionList Empty");
            buildButtons.ToggelAiUIButtons();
        }

        if (points.Count > 0) points.RemoveAt(points.Count - 1);

        if (points.Count < 1)
        {
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i].transform.position);
            }
        }

        positionList.RemoveAt(positionList.Count - 1);
        Debug.Log($"Removed a Position {positionList}");
        aiScript.DefaultMovePositions = positionList;
    }

    public void ConfirmAIBuild()
    {
        SeekerPlacementIndicator.Instance.SpawnObject(buildableObject.prefab.gameObject);
        lineRenderer.positionCount = 0;
        foreach (GameObject point in points)
        {
            Destroy(point);
        }
        points.Clear();
        buildButtons.ToggelAiUIButtons();
    }
}
