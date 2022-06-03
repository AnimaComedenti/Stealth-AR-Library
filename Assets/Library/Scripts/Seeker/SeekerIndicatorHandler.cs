using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Photon.Pun;

namespace StealthLib
{
    public abstract class SeekerIndicatorHandler : MonoBehaviourPun
    {
        [SerializeField] private GameObject _placementIndicator;
        [SerializeField] private Camera _cam;
        [SerializeField] private ARRaycastManager aRRaycastManager;

        protected bool isPlacementValid = false;
        private List<RaycastResult> results = new List<RaycastResult>();
        public Pose PlacementPosition { get; private set; }

        private static SeekerIndicatorHandler _instance = null;
        public static SeekerIndicatorHandler Instance
        {
            get { return _instance; }
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
        public Camera Cam
        {
            get { return _cam; }
            set { _cam = value; }
        }
        public GameObject PlacementIdicator
        {
            get { return _placementIndicator; }
            set { _placementIndicator = value; }
        }

        public void CursorPositionMiddleOfDisplay()
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Vector3 screenCenter = _cam.ViewportToScreenPoint(new Vector3(0.5f, 0.3f));
            aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
            isPlacementValid = hits.Count > 0;

            if (isPlacementValid)
            {
                PlacementPosition = hits[0].pose;
            }
        }
        public void CursorPositionOnTouch()
        {
            if (!IsScreenTouched()) return;
            Touch touch = Input.GetTouch(0);
            Vector3 touchposition = touch.position;
            RaycastHit hit;
            Ray screenCenter = _cam.ScreenPointToRay(touchposition);
            if (Physics.Raycast(screenCenter, out hit))
            {
                Pose newPosition = new Pose(hit.point, Quaternion.identity);
                PlacementPosition = newPosition;
                isPlacementValid = true;
                return;

            }
            isPlacementValid = false;
        }

        public void UpdatePlacementIndicator()
        {
            if (isPlacementValid)
            {
                _placementIndicator.SetActive(true);
                _placementIndicator.transform.SetPositionAndRotation(PlacementPosition.position, PlacementPosition.rotation);
            }
            else
            {
                _placementIndicator.SetActive(false);
            }
        }

        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            results.Clear();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public bool IsScreenTouched()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    if (IsPointerOverUIObject()) return false;
                    return true;
                }
            }
            return false;
        }

        public abstract GameObject SpawnObject(GameObject obj, Quaternion quaternion);
    }
}