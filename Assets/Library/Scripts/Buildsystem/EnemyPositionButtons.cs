using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthDemo.Nodes;
using StealthLib;
using System;
using StealthDemo;

namespace StealthLib
{
    /*
     * Bei dieser Klasse handelt es sich um einem Buttonscript welche das Setzten und Löschen von Positionen sowie das Spawnen des Gegners verarbeitet,
     */
    public class EnemyPositionButtons : MonoBehaviour
    {
        [SerializeField] private GameObject pointPrefab;
        [SerializeField] private GameObject buildButtonsToToggle;
        [SerializeField] private GameObject enemyPostionButtonsToToggle;

        private BuildableObjectSO buildableObject;
        private UIToggler uitoggler;
        private SeekerIndicatorHandler seekerIndicatorHandler;

        public static List<GameObject> points = new List<GameObject>();
        public static List<Vector3> listWithPositions = new List<Vector3>();

        private void Start()
        {
            uitoggler = UIToggler.Instance;
            seekerIndicatorHandler = SeekerIndicatorHandler.Instance;
        }
        public BuildableObjectSO setBuildableObject
        {
            set { buildableObject = value; }
        }

        public void SetAiPositions()
        {
            Pose position = seekerIndicatorHandler.PlacementPosition;
            GameObject circle = Instantiate(pointPrefab, position.position, Quaternion.identity);
            listWithPositions.Add(position.position);
            points.Add(circle);
        }

        public void ResetLastPosition()
        {
            //if removing while no positions, close UI
            if (listWithPositions.Count <= 0)
            {
                Debug.Log("PositionList Empty");
                uitoggler.ToggelUIButtons(buildButtonsToToggle, enemyPostionButtonsToToggle);
                return;
            }

            GameObject lastPoint = points[points.Count - 1];
            Destroy(lastPoint);
            listWithPositions.RemoveAt(listWithPositions.Count - 1);
            points.RemoveAt(points.Count - 1);

        }

        public void ConfirmAIBuild()
        {
            //Spawn enemy
            GameObject ai = seekerIndicatorHandler.SpawnObject(buildableObject.Prefab.gameObject,Quaternion.identity);

            //Set declared positions to move
            EnemyAIBase aiScript = ai.GetComponent<EnemyAIBase>();
            Vector3[] vect3array = listWithPositions.ToArray();
            aiScript.GetComponent<PhotonView>().RPC("AddMovePositions", RpcTarget.AllBuffered, vect3array);

            //Reset everything
            foreach (GameObject point in points)
            {
                Destroy(point);
            }

            listWithPositions.Clear();
            points.Clear();
            uitoggler.ToggelUIButtons(buildButtonsToToggle, enemyPostionButtonsToToggle);
        }

    }
}
