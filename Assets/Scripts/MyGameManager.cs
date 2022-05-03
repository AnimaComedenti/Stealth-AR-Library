using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviourPun
{
    [SerializeField] private GameObject endGameUI;

    public static MyGameManager Instance
    {
        get { return _instance; }
    }

    public bool hasSeekerWon = false;
    public bool hasHiderWon = false;

    private static MyGameManager _instance = null;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSeekerWon)
        {
            photonView.RPC("SeekerWon", RpcTarget.All);
        }
        else if (hasHiderWon)
        {
            photonView.RPC("HiderWon", RpcTarget.All);
        }
    }

    [PunRPC]
    private void HiderWon()
    {
        Debug.Log("Hider won");
    }

    [PunRPC]
    private void SeekerWon()
    {
        Debug.Log("Seeker won");
    }
}
