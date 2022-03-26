using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class ConfirmRotationButton : RotateButton
{
    public static ConfirmRotationButton Instance;
    [SerializeField] ARUIButtonScript aRUI;
    [SerializeField] Material material;
    private BuildableObjectSO objectToBuild;
    private SeekerPlacementIndicator seekerPlacement;
    private Pose placementPosition;

    void Awacke()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
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

    private void ConfirmBuild()
    {
        //Spawn Real Object
        seekerPlacement.SpawnObject(objectToBuild.prefab.gameObject, buildObjectDouble.transform.rotation);

        if (buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);

        //Reset Objects
        buildObjectDouble = null;
        objectToBuild = null;
        aRUI.ToggelRotationMenu();
    }

    public override void OnUiClick()
    {
        ConfirmBuild();
    }
    public void SetObjectToBuild(BuildableObjectSO obj)
    {


        if(buildObjectDouble != null )Destroy(buildObjectDouble);
        if (obj == null) return;
        objectToBuild = obj;

        //Set Duplicate and make colors Transparent
        buildObjectDouble = Instantiate(objectToBuild.prefab, placementPosition.position, placementPosition.rotation);
        SetAlpha(buildObjectDouble.gameObject);
    }

    public  BuildableObjectSO GetObjectToBuild()
    {
        return objectToBuild;
    }
}
