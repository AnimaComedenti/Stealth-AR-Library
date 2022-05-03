using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivatableObject : MonoBehaviour
{
    public abstract void OnActivate();

    public abstract Sprite GetButtonSprite();
}
