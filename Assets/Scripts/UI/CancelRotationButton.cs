using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class CancelRotationButton : RotateButton
{
    [SerializeField] ARUIButtonScript aRUI;
    [SerializeField] private ConfirmRotationButton confirm;
    private BuildableObjectSO objectToBuild;

    
    private void CancelBuild()
    {
        GameObject findGameobject = GameObject.FindGameObjectWithTag(duplicateTag);
        if(findGameobject != null) Destroy(findGameobject);
        confirm.SetObjectToBuild(null);
        aRUI.ToggelRotationMenu();
    }
    public override void OnUiClick()
    {
        CancelBuild();
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
