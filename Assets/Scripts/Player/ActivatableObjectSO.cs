using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ActivatableObjectSO")]
public class ActivatableObjectSO : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] IActivatable activatableItem;
    [SerializeField] Sprite icon;
    [SerializeField] string description;

    public Sprite Icon => icon;
    public string Name => name;
    public string Description => description;
    public IActivatable Prefab => activatableItem;
}
