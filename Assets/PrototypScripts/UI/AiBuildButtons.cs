using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthDemo.Nodes;
using StealthLib;


namespace StealthDemo
{
    public class AiBuildButtons : MonoBehaviour
    {
        [SerializeField] private GameObject pointPrefab;
        [SerializeField] private GameObject buildButtons;
        [SerializeField] private GameObject aiBuildButtons;

        private BuildableObjectSO buildableObject;
        private UIToggler uitoggler;
        private SeekerPlacementIndicator seekerPlacementIndicator;

        public static List<GameObject> points = new List<GameObject>();
        public static List<Vector3> listWithPositions = new List<Vector3>();

        private void Start()
        {
            uitoggler = UIToggler.Instance;
            seekerPlacementIndicator = SeekerPlacementIndicator.Instance;
        }
        public BuildableObjectSO setBuildableObject
        {
            set { buildableObject = value; }
        }

        public void SetAiPositions()
        {
            Pose position = seekerPlacementIndicator.PlacementPosition;
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
                uitoggler.ToggelUIButtons(buildButtons, aiBuildButtons);
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
            GameObject ai = seekerPlacementIndicator.SpawnObject(buildableObject.Prefab.gameObject, Quaternion.identity);

            //Set declared positions to move
            EnemyAI aiScript = ai.GetComponent<EnemyAI>();
            Vector3[] vect3array = listWithPositions.ToArray();
            aiScript.photonV.RPC("AddMovePositions", RpcTarget.AllBuffered, vect3array);

            //Reset everything
            foreach (GameObject point in points)
            {
                Destroy(point);
            }
            listWithPositions.Clear();
            points.Clear();
            uitoggler.ToggelUIButtons(buildButtons, aiBuildButtons);
        }

    }
}