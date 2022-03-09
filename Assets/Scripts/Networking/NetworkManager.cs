using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{

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

    private GameObject[] spawns;
    private bool _isHiderSpawned = false;
    private PhotonView pv;
    private float _hiderCount = 0;

    public float hiderCount
    {
        get { return _hiderCount; }
    }
    public bool isHiderSpawned
    {
        get { return _isHiderSpawned; }
        set { _isHiderSpawned = value; }
    }

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        SpawnSeeker();
    }

    private void SpawnSeeker()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            PhotonNetwork.Instantiate(seekerPrefab.name, Vector3.zero, Quaternion.identity,0);
        }
    }

    private void SpawnHider()
    {
        //Check if player is already spawned and a desktop player
        if (!_isHiderSpawned && SystemInfo.deviceType == DeviceType.Desktop)
        {
            GameObject level = GameObject.FindGameObjectWithTag(levelTag);

            //Only spawn if level exists
            if (level == null) return;
            
            if (isSpawnfixed)
            {
                //spawn on given spawn point
                Vector3 spawnPosition = spawner.transform.position;
                PhotonNetwork.Instantiate(hiderPrefab.name, spawnPosition, Quaternion.identity, 0);
            }
            else
            {
                //Search spawns and spawn on randome position
                spawns = GameObject.FindGameObjectsWithTag(spawnTag);
                int randNmr = Random.Range(0, spawns.Length - 1);
                Vector3 spawnPosition = spawns[randNmr].transform.position;
                PhotonNetwork.Instantiate(hiderPrefab.name, spawnPosition, Quaternion.identity, 0);
            }
            isHiderSpawned = true;
            pv.RPC("CountHiders", RpcTarget.AllBuffered);
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
}
