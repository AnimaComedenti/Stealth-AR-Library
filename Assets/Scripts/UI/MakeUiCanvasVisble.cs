using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MakeUiCanvasVisble : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    [SerializeField] GameObject objectToBeActive;

    private void Awake()
    {
        if (pv.IsMine) objectToBeActive.SetActive(true);
    }

}
