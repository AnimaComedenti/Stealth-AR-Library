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
     * Dieses Script stellt f�r Buttons Methoden bereit welche f�r das bauen von Objekten genutzt werdne kann.
     * 
     * _objectToBuild: Das Objekt welche gebaut werden soll
     * rotatiionButtons: Referenz zum RotationScript um das gebaute vorher rotieren zu k�nne.
     * buildButtons: Das gesamnte Parent-Objekt welches f�r das Ausschalten der UI-Genutzt wird
     */
    public class StandartBuildButton : BuildButton
    {

       /*
        * Methode die an einem Button-OnClick gebunden werden kann.
        * Diese Methode baut das Objekt anhand des BuildHandlers.
        */
        public override void BuildOnClick()
        {
            SeekerIndicatorHandler.Instance.SpawnObject(buildableObject.Prefab.gameObject,Quaternion.identity);
        }

    }
}

