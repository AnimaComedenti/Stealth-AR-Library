using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//TrackScript for AI
public class TrackPlayer : MonoBehaviour
{
    public Camera seekerCamera;
    public GameObject hiderObject;
    public float timeToSeeHider = 30;
    public float timeHiderNotSeen = 20;

    [SerializeField]
    private NetworkManager networkManager;
    [SerializeField]
    private GameResolver gameResolver;
    private float totalHiding;
    private bool isPlayerCatched;
    private PhotonView photonView;

    void Start()
    {
        totalHiding = networkManager.hiderCount;
        photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerCatched = ResolveTimeToSee();
        if (isPlayerCatched)
        {
            CatchPlayer();
        }
    }

    bool ResolveTimeToSee()
    {
        //If player is tracked countdown 
        if (PlayerTrackResolver())
        {
            ///Decrease Time
            
        }
        else
        {
            //Increase time after 20s not seen
        }

        return false;
        
    }

    //Track player
    bool PlayerTrackResolver()
    {
        //Track-Method to find the player
        //Soundsystem should be integrated
        return true;
    }

    void CatchPlayer()
    {
        PhotonNetwork.Destroy(hiderObject);
        photonView.RPC("DecreaseHiders", RpcTarget.AllBuffered);
        totalHiding = networkManager.hiderCount;
        if (totalHiding == 0) gameResolver.didSeekerWin = true;
    }
}
