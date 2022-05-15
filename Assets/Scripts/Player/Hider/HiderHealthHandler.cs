using UnityEngine;
using System.Collections;
using Photon.Pun;
using System;
using UnityEngine.UI;

namespace StealthDemo
{
    public class HiderHealthHandler : MonoBehaviourPun
    {
        public float playerHealth = 1f;

        [SerializeField]
        private Transform healthbarSeeker;
        [SerializeField]private Image healthBarHider;

        private MyGameManager gameManager;
        private float maxhealth;
        private float healthTimer = 5;
        // Use this for initialization
        void Start()
        {
            maxhealth = playerHealth;
            gameManager = MyGameManager.Instance;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (healthTimer > 0) healthTimer -= Time.deltaTime;
            if (healthTimer <= 0) healthTimer = 0;
            UpdateHealthbar();
        }

        void UpdateHealthbar()
        {

            if (playerHealth <= 0)
            {
                playerHealth = maxhealth;
                PhotonNetwork.Destroy(this.gameObject);
                gameManager.hasSeekerWon = true;
            }

            Vector3 oldScale = healthbarSeeker.transform.localScale;
            healthbarSeeker.transform.localScale = new Vector3(playerHealth, oldScale.y, oldScale.z);

            healthBarHider.fillAmount = playerHealth / maxhealth;

            if (healthTimer > 0)
            {
                healthbarSeeker.gameObject.SetActive(true);
            }
            else
            {
                healthbarSeeker.gameObject.SetActive(false);
            }
        }

        public void HitPlayer(float damage)
        {
            photonView.RPC("DamagedPlayer", RpcTarget.All,damage);
        }

        [PunRPC]
        public void DamagedPlayer(float damage)
        {
            healthTimer = 5;
            playerHealth -= damage;
        }
    }
}
