using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class ConfirmRotationButton : RotateButton
{
    [SerializeField] ARUIButtonScript aRUI;
    private BuildableObjectSO objectToBuild;
    private SeekerPlacementIndicator seekerPlacement;
    private Pose placementPosition;
    private Pose lastPlacement;
    private Transform buildObject;
    private Transform buildObjectDouble;

    private void SetAlpha(GameObject obj)
    {
        MeshRenderer[] allChildRenderer = obj.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in allChildRenderer)
        {
            Color color = renderer.material.color;
            renderer.material.color = new Color(color.r, color.g, color.b, 0.6f);
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

    private void ConfirmBuild()
    {
        //Spawn Real Object
        seekerPlacement.SpawnObject(objectToBuild.prefab.gameObject, buildObjectDouble.transform.rotation);
        
        //Reset Objects
        buildObjectDouble = null;
        Destroy(buildObjectDouble);
        objectToBuild = null;
        aRUI.ToggelRotationMenu();
    }

    public override void OnUiClick()
    {
        ConfirmBuild();
    }
    public override void SetObjectToBuild(BuildableObjectSO obj)
    {

        GameObject findingObject = GameObject.FindGameObjectWithTag(duplicateTag);
        if (findingObject != null) Destroy(findingObject);
        if (obj == null) return;
        objectToBuild = obj;

        //Set Duplicate and make colors Transparent
        buildObjectDouble = Instantiate(objectToBuild.prefab, placementPosition.position, placementPosition.rotation);
        buildObjectDouble.tag = duplicateTag;
        SetAlpha(buildObjectDouble.gameObject);
    }

    public override BuildableObjectSO GetObjectToBuild()
    {
        return objectToBuild;
    }
}
