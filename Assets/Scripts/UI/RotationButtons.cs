using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class RotationButtons : MonoBehaviour
{
    public static Transform buildObjectDouble;

    [SerializeField] private Material material;
    [SerializeField] private GameObject buildButtons;
    private BuildableObjectSO objectToBuild;
    private SeekerPlacementIndicator seekerPlacement;
    private Pose placementPosition;
    

    private void SetAlpha(GameObject obj)
    {
        MeshRenderer[] allChildRenderer = obj.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in allChildRenderer)
        {
            renderer.material = material;
        }
    }

    private void Start()
    {
        seekerPlacement = SeekerPlacementIndicator.Instance;
    }
    void Update()
    {
        if (buildObjectDouble == null) return;
        buildObjectDouble.transform.position = seekerPlacement.getPlacementPosition.position;
    }

    public void RotateObjectLeft()
    {
        if (buildObjectDouble == null) return;
        buildObjectDouble.transform.Rotate(Vector3.up, 20f);
    }

    public void RotateObjectRight()
    {
        if (buildObjectDouble == null) return;
        buildObjectDouble.transform.Rotate(Vector3.up, -20f);
    }

    public void CancelBuild()
    {
        if (buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);
        SetObjectToBuild(null);
        UIToggler.Instance.ToggelUIButtons(this.gameObject, buildButtons);
    }


    public void ConfirmRotation()
    {
        //Spawn Real Object
        seekerPlacement.SpawnObject(objectToBuild.Prefab.gameObject, buildObjectDouble.transform.rotation);

        if (buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);

        //Reset Objects
        buildObjectDouble = null;
        objectToBuild = null;
        UIToggler.Instance.ToggelUIButtons(this.gameObject, buildButtons);
    }

    public void SetObjectToBuild(BuildableObjectSO obj)
    {


        if(buildObjectDouble != null )Destroy(buildObjectDouble);
        if (obj == null) return;
        objectToBuild = obj;

        //Set Duplicate and make colors Transparent
        buildObjectDouble = Instantiate(objectToBuild.Prefab, placementPosition.position, placementPosition.rotation);
        SetAlpha(buildObjectDouble.gameObject);
    }

    public  BuildableObjectSO GetObjectToBuild()
    {
        return objectToBuild;
    }
}
