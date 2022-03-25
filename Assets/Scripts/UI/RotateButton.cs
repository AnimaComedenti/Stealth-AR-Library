using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthARLibrary;

public abstract class RotateButton: MonoBehaviour
{
    public string duplicateTag = "Duplicate";
    public abstract void OnUiClick();

    public abstract void SetObjectToBuild(BuildableObjectSO obj);

    public abstract BuildableObjectSO GetObjectToBuild();

}

