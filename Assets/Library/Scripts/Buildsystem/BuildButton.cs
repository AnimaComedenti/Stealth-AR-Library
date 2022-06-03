using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;
using System;
using StealthDemo;

namespace StealthLib
{
    /*
     * Bei dieser Klasse handelt es sich um ein BuildButton.
     * Dieses Script stellt für Buttons Methoden bereit welche für das bauen von Objekten genutzt werdne kann.
     * 
     * _objectToBuild: Das Objekt welche gebaut werden soll
     * rotatiionButtons: Referenz zum RotationScript um das gebaute vorher rotieren zu könne.
     * buildButtons: Das gesamnte Parent-Objekt welches für das Ausschalten der UI-Genutzt wird
     */
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private BuildableObjectSO _objectToBuild;
        [SerializeField] private RotationButtons rotationButtonsToToggle;
        [SerializeField] private GameObject BuildButtonsToToggle;

        void Start()
        {
            AddSpritesToImages(_objectToBuild.Sprite);
        }
       /*
        * Methode die an einem Button-OnClick gebunden werden kann.
        * Diese Methode baut das Objekt anhand des BuildHandlers.
        */
        public void BuildOnClick()
        {
            SeekerIndicatorHandler.Instance.SpawnObject(_objectToBuild.Prefab.gameObject,Quaternion.identity);
        }

        /*
         * Methode die an einem Button-OnClick gebunden werden kann.
         * Diese Methode öffnet nach dem Klicken das Rotation-UI.
         */
        public void RotateBeforeBuild()
        {
            if (rotationButtonsToToggle == null) return;

            rotationButtonsToToggle.SetObjectToBuild(_objectToBuild);
            UIToggler.Instance.ToggelUIButtons(BuildButtonsToToggle, rotationButtonsToToggle.gameObject);
        }

        /*
         * Setzt das Bild des Buttons anhan des Sprites vom BuildObjekt
         */
        private void AddSpritesToImages(Sprite sprite)
        {
            Image image = transform.GetChild(1).GetComponent<Image>();
            image.sprite = sprite;
        }

    }
}

