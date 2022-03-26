using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class CancelRotationButton : RotateButton
{
    [SerializeField] ARUIButtonScript aRUI;
    [SerializeField] private ConfirmRotationButton confirm;
    
    private void CancelBuild()
    {
        if(buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);
        confirm.SetObjectToBuild(null);
        aRUI.ToggelRotationMenu();
    }
    public override void OnUiClick()
    {
        CancelBuild();
    }
}
