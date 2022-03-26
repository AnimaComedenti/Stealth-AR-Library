using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
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

        private Pose placementPosition;
        private Camera _cam;
        private GameObject _placementIdicator;

        private GameObject _player;
        private List<RaycastResult> results = new List<RaycastResult>();
        private bool isPlacementValid = false;
        private bool isLevelPlaced = false;
        private bool hasHitLevel = false;

        public Camera cam
        {
            get { return _cam; }
            set { _cam = value; }
        }
        public GameObject placementIdicator
        {
            get { return _placementIdicator; }
            set { _placementIdicator = value; }
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
        }

        public virtual void FixedUpdate()
        {
            if (cam == null || placementIdicator == null) return;
            if (photonView.IsMine)
            {
                if (!isLevelPlaced) UpdateLevelCursorPosition();
                UpdateBuildingCursorPosition();
                UpdatePlacementIndicator();
            }
        }

        #region Cursor 
        void UpdateLevelCursorPosition()
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Vector3 screenCenter = _cam.ViewportToScreenPoint(new Vector3(0.5f, 0.3f));
            aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
            isPlacementValid = hits.Count > 0;

            if (isPlacementValid)
            {
                placementPosition = hits[0].pose;
            }
        }
        private void UpdateBuildingCursorPosition()
        {
            if (!isLevelPlaced) return;
            if (!IsScreenTouched()) return;
            Touch touch = Input.GetTouch(0);
            Vector3 touchposition = touch.position;
            RaycastHit hit;
            Ray screenCenter = _cam.ScreenPointToRay(touchposition);
            if (Physics.Raycast(screenCenter, out hit))
            {
                if (hit.transform.gameObject.layer == levelLayerInt)
                {
                    Pose newPosition = new Pose(hit.point, Quaternion.identity);
                    placementPosition = newPosition;
                    isPlacementValid = true;
                    hasHitLevel = true;
                    return;
                }

            }
            isPlacementValid = false;
            hasHitLevel = false;
        }

        private void UpdatePlacementIndicator()
        {
            if (isPlacementValid)
            {
                _placementIdicator.SetActive(true);
                _placementIdicator.transform.SetPositionAndRotation(placementPosition.position, placementPosition.rotation);
            }
            else
            {
                _placementIdicator.SetActive(false);
            }
        }
        #endregion

        #region TouchFunctionalities
        public bool IsScreenTouched()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchposition = touch.position;
                if (touch.phase == TouchPhase.Began)
                {
                    if (IsPointerOverUIObject()) return false;
                    return true;
                }
            }
            return false;
        }

        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            results.Clear();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        #endregion

        #region BuildMethods
        public GameObject SpawnObject(GameObject obj, Quaternion quaternion)
        {
            GameObject objToBuild;
            if (!isPlacementValid) return null;
            if (obj.CompareTag(levelTag) && !isLevelPlaced)
            {
                objToBuild = PhotonNetwork.Instantiate(obj.gameObject.name, placementPosition.position, quaternion);
                NetworkManager.Instance.photonView.RPC("BuildNavMesh", RpcTarget.AllBuffered);
                isLevelPlaced = true;
                return objToBuild;

            }
            else if (hasHitLevel && !obj.CompareTag(levelTag))
            {
                objToBuild = PhotonNetwork.Instantiate(obj.gameObject.name, placementPosition.position, quaternion);
                NetworkManager.Instance.photonView.RPC("BuildNavMesh", RpcTarget.AllBuffered);
                return objToBuild;
            }
            return null;

        }
        #endregion
    }
}

