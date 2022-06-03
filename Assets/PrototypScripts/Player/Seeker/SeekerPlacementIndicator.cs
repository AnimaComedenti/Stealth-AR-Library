using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using StealthLib;

namespace StealthDemo
{
    public class SeekerPlacementIndicator : SeekerIndicatorHandler
    {
        [SerializeField] private BuildHandler buildHandler;
        [SerializeField] private string levelTag = "Level";

        public bool isLevelPlaced { get; private set; } = false;


        public void FixedUpdate()
        {
            if (Cam == null || PlacementIdicator == null) return;

            if (photonView.IsMine)
            {
                if (!isLevelPlaced)
                {
                    CursorPositionMiddleOfDisplay();
                }
                else
                {
                    CursorPositionOnTouch();
                }

                UpdatePlacementIndicator();
            }
        }

        public override GameObject SpawnObject(GameObject obj, Quaternion quaternion)
        {
            GameObject objToBuild;
            if (!isPlacementValid) return null;
            if (obj.CompareTag(levelTag) && !isLevelPlaced)
            {
                objToBuild = buildHandler.SpawnObject(PlacementPosition,obj,quaternion);
                isLevelPlaced = true;
                return objToBuild;

            }
            else if (isLevelPlaced)
            {
                objToBuild = buildHandler.SpawnObject(PlacementPosition, obj, quaternion);
                return objToBuild;
            }
            return null;
        }
    }
}

