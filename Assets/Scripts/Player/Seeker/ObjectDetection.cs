using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public bool IsObjectSeen { get; private set; } = false;

    [SerializeField] private Camera cam;
    [SerializeField] private string objectTag;

    private GameObject[] objectsFound;

    void Update()
    {
        SearchObject();
        if (objectsFound.Length <= 0) return;

        foreach(GameObject searchedObject in objectsFound)
        {
            Renderer render = searchedObject.GetComponent<Renderer>();
            if (render.isVisible)
            {
                CheckRendererInSigth(searchedObject.transform.position);
            }
        }
    }

    private void CheckRendererInSigth(Vector3 objectPosition)
    {
        RaycastHit hit;
        if (Physics.Linecast(cam.transform.position, objectPosition, out hit))
        {
            if(hit.transform.CompareTag(objectTag))
            {
                Debug.Log("Tracked Object found");
                IsObjectSeen = true;
                return;
            }
            Debug.Log("Tracked Object not found");
            IsObjectSeen = false;
            return;
        }
    }

    private void SearchObject()
    {
        objectsFound = GameObject.FindGameObjectsWithTag(objectTag);
    }
}
