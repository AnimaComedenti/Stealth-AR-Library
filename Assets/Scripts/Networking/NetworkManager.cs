using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
using Photon.Pun;


namespace StealthDemo
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        
        [SerializeField]
        private GameObject hiderPrefab;
        [SerializeField]
        private GameObject seekerCam;
        [Header("Spawn options for hider")]
        [SerializeField]
        private bool isSpawnfixed;
        [SerializeField]
        private string levelTag = "Level";
        [SerializeField]
        private string spawnTag = "Spawn";
        [SerializeField]
        private Transform cameraOffset;
        [SerializeField]
        private ARSessionOrigin arSessionOrigin;

        private GameObject[] spawns;
        private bool _isHiderSpawned = false;

        public void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.DestroyAll();
            }
            SpawnSeeker();
        }

        private void SpawnSeeker()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                GameObject seekerCamera = PhotonNetwork.Instantiate(seekerCam.name, Vector3.zero, Quaternion.identity,0);
                photonView.RPC("AttachParentOnSpawn", RpcTarget.AllBuffered, seekerCamera.GetComponent<PhotonView>().ViewID);
                Camera cam = seekerCamera.GetComponent<Camera>();
                GameObject seekerIndi = seekerCamera.transform.GetChild(0).gameObject;
                SeekerPlacementIndicator.Instance.cam = cam;
                arSessionOrigin.camera = cam;
                SeekerPlacementIndicator.Instance.placementIdicator = seekerIndi;
            }
        }


        private void SpawnHider()
        {
            //Check if player is already spawned and a desktop player
            if (!_isHiderSpawned && SystemInfo.deviceType == DeviceType.Desktop)
            {
                GameObject level = GameObject.FindGameObjectWithTag(levelTag);
                GameObject hider;
                //Only spawn if level exists
                if (level == null) return;

                if (isSpawnfixed)
                {
                    //spawn on given spawn point
                    spawns = GameObject.FindGameObjectsWithTag(spawnTag);
                    Vector3 spawnPosition = spawns[0].transform.position;
                    hider = PhotonNetwork.Instantiate(hiderPrefab.name, spawnPosition, Quaternion.identity, 0);
                }
                else
                {
                    //Search spawns and spawn on randome position
                    spawns = GameObject.FindGameObjectsWithTag(spawnTag);
                    int randNmr = Random.Range(0, spawns.Length - 1);
                    Vector3 spawnPosition = spawns[randNmr].transform.position;
                    hider = PhotonNetwork.Instantiate(hiderPrefab.name, spawnPosition, Quaternion.identity, 0);
                }
                isHiderSpawned = true;
                photonView.RPC("AttachParentOnSpawn", RpcTarget.AllBuffered, hider.GetComponent<PhotonView>().ViewID);
            }
        }

        private void Update()
        {
            SpawnHider();
        }



        [PunRPC]
        public void AttachParentOnSpawn(int viewID)
        {
            GameObject objectToSpawn = PhotonNetwork.GetPhotonView(viewID).gameObject;
            if (objectToSpawn.transform.CompareTag("Seeker"))
            {
                objectToSpawn.transform.parent = cameraOffset;
            }
        }

        public bool isHiderSpawned
        {
            get { return _isHiderSpawned; }
            set { _isHiderSpawned = value; }
        }
    }
}
