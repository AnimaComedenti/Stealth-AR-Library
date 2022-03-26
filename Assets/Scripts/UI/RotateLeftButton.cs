using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public class RotateLeftButton : RotateButton
{
    private void RotateObjectLeft()
    {
        if (buildObjectDouble == null) return;
        buildObjectDouble.transform.Rotate(Vector3.up,20f);
    }

    public override void OnUiClick()
    {
        RotateObjectLeft();
    }
}
