using UnityEngine;
using System.Collections;

public class CombatButton : ARInteractionButtons
{
    [SerializeField] private ActivatableObject activatable;
    // Use this for initialization
    void Start()
    {
        AddSpritesToImages(activatable.GetButtonSprite());
    }

    public void OnClick()
    {
        activatable.OnActivate();
    }
}
