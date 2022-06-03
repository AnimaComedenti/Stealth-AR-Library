using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthLib;
using System;
using StealthDemo;

namespace StealthLib
{
    /*
     * Das Rotations-Menü welches das rotieren des Objektes ermöglicht.
     * Hierbei wird ein Klon des zu bauendem Objektes erzeugt und dessen Rotation verändert.
     * Bei bestätigung werden die Transformationsdaten für das bauen geholt, der Klon gelöscht und das Objekt gebaut
     * 
     * material: Das Material welches das aussehen des Klons bestimmt
     * buildButtonToToggle: Zum zurücktoggeln um zur vorherigen Sicht zu gelangen.
     * rotationButtonsToToggle: Diesse Menu welches nach dem Bauen unactiv gemacht wird
     */
    public class RotationButtons : MonoBehaviour
    {
        public static Transform buildObjectDouble;

        [SerializeField] private Material material;
        [SerializeField] private GameObject buildButtonToToggle;
        [SerializeField] private GameObject rotationButtonsToToggle;

        private BuildableObjectSO objectToBuild;
        private Pose placementPosition;

        /*
         * Methode welche das Object Klont
         * 
         * obj: Das Objekt welches geklont sowie gebaut werden soll
         */
        public void SetObjectToBuild(BuildableObjectSO obj)
        {
            if (buildObjectDouble != null) Destroy(buildObjectDouble);
            if (obj == null) return;
            objectToBuild = obj;

            //Set Duplicate and make colors Transparent
            buildObjectDouble = Instantiate(objectToBuild.Prefab, placementPosition.position, placementPosition.rotation);
            SetMaterial(buildObjectDouble.gameObject);
        }

        /*
         * Methode welche alle Childobjekte des Objektes welches zu bauen ist dem Material zufügt
         * 
         * obj: Gameobject welches geklont und gebaut werden soll
         */
        private void SetMaterial(GameObject obj)
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

        /*
         * Rotationmethode für das links Rotieren
         */
        public void RotateObjectLeft()
        {
            if (buildObjectDouble == null) return;
            buildObjectDouble.transform.Rotate(Vector3.up, 20f);
        }

        /*
         * Rotationmethode für das rechts Rotieren
         */
        public void RotateObjectRight()
        {
            if (buildObjectDouble == null) return;
            buildObjectDouble.transform.Rotate(Vector3.up, -20f);
        }

        /*
         * Methode um das Bauen abzubrechen
         */
        public void CancelBuild()
        {
            if (buildObjectDouble != null) Destroy(buildObjectDouble.gameObject);
            SetObjectToBuild(null);
            UIToggler.Instance.ToggelUIButtons(rotationButtonsToToggle, buildButtonToToggle);
        }



        /*
         * Methode um das Bauen zubestätigen und das Objekt baut.
         */
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

    }
}
