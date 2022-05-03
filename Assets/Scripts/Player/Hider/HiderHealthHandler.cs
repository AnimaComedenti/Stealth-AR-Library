using UnityEngine;
using System.Collections;
using Photon.Pun;
using System;

public class HiderHealthHandler : MonoBehaviourPun, IPunObservable
{
    public float playerHealth = 1f;

    [SerializeField]
    private Transform healthbar;
    private float maxhealth;
    private float healthTimer = 5;
    // Use this for initialization
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop && photonView.IsMine)
        {
            maxhealth = playerHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthTimer > 0) healthTimer -= Time.deltaTime;
    }

    void UpdateHealthbar()
    {
        healthbar.gameObject.SetActive(true);
        Vector3 oldScale = healthbar.transform.localScale;
        healthbar.transform.localScale = new Vector3(playerHealth, oldScale.y, oldScale.z);

        if (healthTimer > 0)
        {
            healthbar.gameObject.SetActive(true);
        }
        else
        {
            healthbar.gameObject.SetActive(false);
        }
    }

    public void HitPlayer(float damage)
    {
        if (!photonView.IsMine) return;
        healthTimer = 5;
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            playerHealth = maxhealth;
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerHealth);
        }
        else
        {
            playerHealth = (float)stream.ReceiveNext();
            UpdateHealthbar();
        }
    }
}
