using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviourPun
{
    [SerializeField] float damage;
    [SerializeField] float travelTime;


    // Update is called once per frame
    void Update()
    {
        travelTime -= Time.deltaTime;
        if (travelTime <= 0)
        {
            photonView.RPC("killBullet", RpcTarget.All);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            return;
        }
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HiderHealthHandler>().HitPlayer(damage);
            photonView.RPC("killBullet", RpcTarget.All);
        }
    }

    [PunRPC]
    public void killBullet()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
