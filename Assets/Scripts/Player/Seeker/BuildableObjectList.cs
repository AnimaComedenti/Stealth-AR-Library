using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthARLibrary
{
    [CreateAssetMenu(menuName = ("ScriptableObjects/BuildableObjectsListSO"))]
    public class BuildableObjectList : ScriptableObject
    {
        public BuildableObjectSO levelprefab;
        public List<BuildableObjectSO> objectList;
    }
}

