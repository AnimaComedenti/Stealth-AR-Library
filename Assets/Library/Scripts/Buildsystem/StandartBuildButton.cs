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

