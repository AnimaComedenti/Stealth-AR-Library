using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementIndicator : MonoBehaviour
{

    public GameObject levelToSpawn;
    public GameObject placementIdicator;
    public Camera cam;
    //could be Private
    public GameObject[] gameObjectsToSpawnInLevel;

    private GameObject spawnedGameObject;
    private Pose placementPosition;
    private ARRaycastManager aRRaycastManager;
    private bool isLevelPlaced = false;
    private bool isPlacementValid = false;

    void Awake()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceGameObject();
        }
        UpdatePlacementPosition();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if(spawnedGameObject == null && isPlacementValid)
        {
            placementIdicator.SetActive(true);
            placementIdicator.transform.SetPositionAndRotation(placementPosition.position, placementPosition.rotation);
        }
        else
        {
            placementIdicator.SetActive(false);
        }
    }

    private void UpdatePlacementPosition()
    {
        var screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.3f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        isPlacementValid = hits.Count > 0;
        if (isPlacementValid)
        {
            placementPosition = hits[0].pose;
        }
    }

    private void PlaceGameObject()
    {
        if(isPlacementValid && !isLevelPlaced)
        {
            //Place LevelObject
            spawnedGameObject = Instantiate(levelToSpawn, placementPosition.position, placementPosition.rotation);
            //isLevelPlaced = true;
        }
        else if (isPlacementValid)
        {
            //Buildsystem
            spawnedGameObject = Instantiate(levelToSpawn, placementPosition.position, placementPosition.rotation);
        }
    }

    //Getter and Setters
}
