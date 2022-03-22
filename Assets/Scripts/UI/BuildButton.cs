using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StealthARLibrary
{
    public class BuildButton : MonoBehaviour
    {
        //TODO: Deactivate Touch on Ui click
        [SerializeField] private string enemyTag = "Enemy";

        public BuildableObjectSO ObjectToSpawn
        {
            set { _objectToBuild = value; }
            get { return _objectToBuild; }
        }

        private BuildableObjectSO _objectToBuild;
        private ARUIButtonScript buildButtons;
        

        private void Start()
        {
            buildButtons = FindObjectOfType<ARUIButtonScript>();
            
        }

        public void BuildOnClick()
        {
            if (_objectToBuild == null) return;

            if (_objectToBuild.prefab.gameObject.CompareTag(enemyTag))
            {
                /*AiBuildButtons[] aiBuild = buildButtons.getARUIButtons;
                foreach(AiBuildButtons button in aiBuild)
                {
                    button.setBuildableObject = _objectToBuild;
                }*/
                buildButtons.ToggelAiUIButtons();  
            }

            if (buildButtons.IsAiBuilded) SeekerPlacementIndicator.Instance.SpawnObject(_objectToBuild.prefab.gameObject);
        }
    }
}

