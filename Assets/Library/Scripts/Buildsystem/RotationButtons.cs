using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthLib;
using System;
using StealthDemo;

namespace StealthLib
{

    public class RotationButtons : MonoBehaviour
    {
        public static Transform buildObjectDouble;

        [SerializeField] private Material material;
        [SerializeField] private GameObject buildButtonToToggle;
        [SerializeField] private GameObject rotationButtonsToToggle;
        private BuildableObjectSO objectToBuild;
        private Pose placementPosition;


        private void SetAlpha(GameObject obj)
        {
            MeshRenderer[] allChildRenderer = obj.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in allChildRenderer)
            {
                renderer.material = material;
            }
        }

        void Update()
        {
            if (buildObjectDouble == null) return;
            buildObjectDouble.transform.position = SeekerIndicatorHandler.Instance.PlacementPosition.position;
        }

        public void RotateObjectLeft()
        {
            if (buildObjectDouble == null) return;
            buildObjectDouble.transform.Rotate(Vector3.up, 20f);
        }

        public void RotateObjectRight()
        {
            if (buildObjectDouble == null) return;
            buildObjectDouble.transform.Rotate(Vector3.up, -20f);
        }

        public void CancelBuild()
        {
            if (buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);
            SetObjectToBuild(null);
            UIToggler.Instance.ToggelUIButtons(rotationButtonsToToggle, buildButtonToToggle);
        }


        public void ConfirmRotation()
        {
            //Spawn Real Object
            SeekerIndicatorHandler.Instance.SpawnObject(objectToBuild.Prefab.gameObject, buildObjectDouble.transform.rotation);

            if (buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);

            //Reset Objects
            buildObjectDouble = null;
            objectToBuild = null;
            UIToggler.Instance.ToggelUIButtons(rotationButtonsToToggle, buildButtonToToggle);
        }

        public void SetObjectToBuild(BuildableObjectSO obj)
        {
            if (buildObjectDouble != null) Destroy(buildObjectDouble);
            if (obj == null) return;
            objectToBuild = obj;

            //Set Duplicate and make colors Transparent
            buildObjectDouble = Instantiate(objectToBuild.Prefab, placementPosition.position, placementPosition.rotation);
            SetAlpha(buildObjectDouble.gameObject);
        }

        public BuildableObjectSO GetObjectToBuild()
        {
            return objectToBuild;
        }
    }
}
