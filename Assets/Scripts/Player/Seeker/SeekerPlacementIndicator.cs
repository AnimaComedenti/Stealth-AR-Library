using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Utils;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace StealthARLibrary
{
    public class SeekerPlacementIndicator : MonoBehaviourPun
    {

        private static SeekerPlacementIndicator _instance = null;

        public static SeekerPlacementIndicator Instance
        {
            get { return _instance; }
        }
        
        [SerializeField] private int levelLayerInt = 3;
        [SerializeField] private string levelTag = "Level";
        [SerializeField] private ARRaycastManager aRRaycastManager;
        [SerializeField] private ARSessionOrigin origin;


        private Pose placementPosition;
        private NavMeshSurface levelMash;
        private Camera cam;
        private GameObject placementIdicator;

        private GameObject _player;

        private bool isPlacementValid = false;
        private bool isLevelPlaced = false;
        private bool hasHitLevel = false;
        private PhotonView networkView;

        public Camera Cam
        {
            get { return cam; }
            set { cam = value; }
        }
        public GameObject Indicator
        {
            get { return placementIdicator; }
            set { placementIdicator = value; }
        }
        public Pose getPlacementPosition
        {
            get { return placementPosition; }
        }


        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            } 

            if (photonView.IsMine && SystemInfo.deviceType == DeviceType.Handheld)
            {
                networkView = NetworkManager.Instance.photonView;
            }
        }

        public virtual void FixedUpdate()
        {
            if (cam == null || placementIdicator == null) return;
            Debug.Log(cam.transform.position);
            if (photonView.IsMine)
            {
                if (!isLevelPlaced) UpdateLevelCursorPosition();
                UpdateBuildingCursorPosition();
                UpdatePlacementIndicator();
            }
        }

        private void UpdatePlacementIndicator()
        {
            if (isPlacementValid)
            {
                placementIdicator.SetActive(true);
                placementIdicator.transform.SetPositionAndRotation(placementPosition.position, placementPosition.rotation);
            }
            else
            {
                placementIdicator.SetActive(false);
            }
        }

        void UpdateLevelCursorPosition()
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Vector3 screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.3f));
            aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
            isPlacementValid = hits.Count > 0;

            if (isPlacementValid)
            {
                placementPosition = hits[0].pose;
            }
        }
        private void UpdateBuildingCursorPosition()
        {
            //if (IsPointerOverUIObject()) return;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 touchposition = touch.position;

                if(touch.phase == TouchPhase.Began)
                {
                    if(isLevelPlaced)
                    {
                        RaycastHit hit;
                        Ray screenCenter = cam.ScreenPointToRay(touchposition);
                        if (Physics.Raycast(screenCenter, out hit))
                        {
                            if (hit.transform.gameObject.layer == levelLayerInt)
                            {
                                Pose newPosition = new Pose(hit.point, Quaternion.identity);
                                placementPosition = newPosition;
                                isPlacementValid = true;
                                hasHitLevel = true;
                            }
                            else
                            {
                                isPlacementValid = false;
                                hasHitLevel = false;
                            }

                        }
                        else
                        {
                            Debug.Log("Raycasthit didnt hit anything");
                            isPlacementValid = false;
                            hasHitLevel = false;
                        }
                    }
                }   
            }
        }

       public void SpawnObject(GameObject obj)
        {
            GameObject obcjToSpawn;
            if (isPlacementValid && obj.CompareTag(levelTag) && !isLevelPlaced)
            {
                levelMash = obj.GetComponent<NavMeshSurface>();
                obcjToSpawn = PhotonNetwork.Instantiate(obj.gameObject.name, placementPosition.position, Quaternion.identity);
                networkView.RPC("AttachParentOnSpawn", RpcTarget.AllBuffered, obcjToSpawn.GetComponent<PhotonView>().ViewID);
                //origin.MakeContentAppearAt(obcjToSpawn.transform,new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-10));
                isLevelPlaced = true;
            }
            else if (isPlacementValid && hasHitLevel)
            {
                obcjToSpawn = PhotonNetwork.Instantiate(obj.gameObject.name, placementPosition.position, Quaternion.identity);
                networkView.RPC("AttachParentOnSpawn", RpcTarget.AllBuffered, obcjToSpawn.GetComponent<PhotonView>().ViewID);
                NavMeshSurface meshFromObject;
                meshFromObject = obj.GetComponent<NavMeshSurface>();
                if (meshFromObject != null) meshFromObject.BuildNavMesh();
            }
            if(isLevelPlaced) levelMash.BuildNavMesh();
        }


        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}

