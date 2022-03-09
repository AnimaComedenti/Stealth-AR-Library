using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

namespace StealthARLibrary
{
    public class BuildButton : MonoBehaviour
    {
        //TODO: Deactivate Toiuch on Ui click
        public BuildableObjectSO ObjectToSpawn
        {
            set { _objectToBuild = value; }
            get { return _objectToBuild; }
        }

        [SerializeField] private SeekerPlacementIndicator seekerPlacementIndicator;
        private BuildableObjectSO _objectToBuild;

        public void BuildOnClick()
        {
            if (_objectToBuild == null)
            {
                Debug.LogError("SpawnableObject is Null");
                return;
            }
            seekerPlacementIndicator.SpawnObject(_objectToBuild);
        }
    }
}

