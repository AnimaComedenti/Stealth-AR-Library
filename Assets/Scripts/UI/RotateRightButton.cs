using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;
public class RotateRightButton : RotateButton
{
    private float rotationY;
    private BuildableObjectSO objectToBuild;
    
    private void RotateObjectRight()
    {
        GameObject findingObject = GameObject.FindGameObjectWithTag(duplicateTag);
        if (findingObject == null) return;
        findingObject.transform.Rotate(Vector3.up, -20f);
    }

    public override void OnUiClick()
    {
        RotateObjectRight();
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
