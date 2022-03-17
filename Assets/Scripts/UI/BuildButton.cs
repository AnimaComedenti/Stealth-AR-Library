using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StealthARLibrary
{
    public class BuildButton : MonoBehaviour
    {
        //TODO: Deactivate Touch on Ui click
        public BuildableObjectSO ObjectToSpawn
        {
            set { _objectToBuild = value; }
            get { return _objectToBuild; }
        }

        private BuildableObjectSO _objectToBuild;

        public void BuildOnClick()
        {
            if (_objectToBuild == null)
            {
                Debug.LogError("SpawnableObject is Null");
                return;
            }
            SeekerPlacementIndicator.Instance.SpawnObject(_objectToBuild);
        }
    }
}

