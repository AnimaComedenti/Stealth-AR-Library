using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthARLibrary
{
    [CreateAssetMenu(menuName ="ScriptableObjects/BuildableObjectsSO")]
    public class BuildableObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
    }
}

