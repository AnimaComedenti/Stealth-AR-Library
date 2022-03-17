using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using StealthARLibrary;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class NetworkManager : MonoBehaviourPunCallbacks
{

    private static NetworkManager _instance = null;

    public static NetworkManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private GameObject hiderPrefab;
    [SerializeField]
    private GameObject seekerPrefab;
    [Header("Spawn options for hider")]
    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private bool isSpawnfixed;
    [SerializeField]
    private string levelTag = "Level";
    [SerializeField]
    private string spawnTag = "Spawn";
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private ARSessionOrigin arSessionOrigin;


    private GameObject[] spawns;
    private bool _isHiderSpawned = false;
    private PhotonView pv;
    private float _hiderCount = 0;



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        SpawnSeeker();
    }

    private void SpawnSeeker()
    {
        GameObject seeker;
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            seeker = PhotonNetwork.Instantiate(seekerPrefab.name, Vector3.zero, Quaternion.identity,0);
            pv.RPC("AttachParentOnSpawn", RpcTarget.AllBuffered, seeker.GetComponent<PhotonView>().ViewID);
            Camera cam = seeker.GetComponentInChildren<Camera>();
            arSessionOrigin.camera = cam;
            SeekerPlacementIndicator.Instance.Cam = cam;
            SeekerPlacementIndicator.Instance.Indicator = seeker.transform.GetChild(0).gameObject;
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
                Vector3 spawnPosition = spawner.transform.position;
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
            pv.RPC("CountHiders", RpcTarget.AllBuffered);
            pv.RPC("AttachParentOnSpawn",RpcTarget.AllBuffered, hider.GetComponent<PhotonView>().ViewID);
        }
    }

    private void Update()
    {
        SpawnHider();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            pv.RPC("DecreaseHiders", RpcTarget.AllBuffered);
        }
    }

    //Count hidercounter up for gameresolving
    [PunRPC]
    public void CountHiders()
    {
        _hiderCount++;
        Debug.Log("New Hider created. Total Hiders: " + hiderCount);
    }

    [PunRPC]
    public void DecreaseHiders()
    {
        if(hiderCount >= 1) _hiderCount--;
        Debug.Log("Hider Removed. Total Hiders: " + hiderCount);
    }

    [PunRPC]
    public void AttachParentOnSpawn(int viewID)
    {
        Debug.Log("Attach"); 
        GameObject objectToSpawn = PhotonNetwork.GetPhotonView(viewID).gameObject;
        objectToSpawn.transform.parent = parent;
    }

    public PhotonView photonView
    {
        get { return pv; }
    }

    public float hiderCount
    {
        get { return _hiderCount; }
    }
    public bool isHiderSpawned
    {
        get { return _isHiderSpawned; }
        set { _isHiderSpawned = value; }
    }
}
