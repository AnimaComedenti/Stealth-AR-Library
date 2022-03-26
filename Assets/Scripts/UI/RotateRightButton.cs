using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;
public class RotateRightButton : RotateButton
{  
    private void RotateObjectRight()
    {
        if (buildObjectDouble == null) return;
        buildObjectDouble.transform.Rotate(Vector3.up, -20f);
    }

    public override void OnUiClick()
    {
        RotateObjectRight();
    }
}
