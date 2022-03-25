using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class RotateLeftButton : RotateButton
{
    private BuildableObjectSO objectToBuild;

    private void RotateObjectLeft()
    {
        GameObject findingObject = GameObject.FindGameObjectWithTag(duplicateTag);
        if (findingObject == null) return;
        findingObject.transform.Rotate(Vector3.up,20f);
    }

    public override void OnUiClick()
    {
        RotateObjectLeft();
    }

    public override void SetObjectToBuild(BuildableObjectSO obj)
    {
        objectToBuild = obj;
    }

    public override BuildableObjectSO GetObjectToBuild()
    {
        return objectToBuild;
    }
}
