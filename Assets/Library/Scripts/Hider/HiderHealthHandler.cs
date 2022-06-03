using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace StealthLib
{
    /*
     * Klasse die, die Veränderungen des Spielerlebends verarbeitet
     * Diese Klasse wird beim abziehen sowie hinzufügen von Leben aufgerufen
     */
    public class HiderHealthHandler : MonoBehaviourPun
    {
        public float playerHealth = 1f;
        private float maxhealth;

        void Start()
        {
            maxhealth = playerHealth;
        }

        void FixedUpdate()
        {
            UpdateHealthbar();
        }

        void UpdateHealthbar()
        {

            if (playerHealth <= 0)
            {
                playerHealth = maxhealth;
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        /*
         * Methode welche das Leben des Spielers abzieht
         * 
         * damage: Wert der abgezogen werden soll
         */
        public void HitPlayer(float damage)
        {
            if (damage < 0)
            {
                damage *= -1;
            }
            photonView.RPC("ChangePlayerHealth", RpcTarget.All, damage);
        }

        /*
         * Methode welche das Leben des Spielers hinzufügt
         * 
         * healAmount: Wert der hinzugefügt werden soll
         */
        public void HealPlayer(float healAmount)
        {
            if(healAmount > 0)
            {
                healAmount *= -1;
            }
            photonView.RPC("ChangePlayerHealth", RpcTarget.All, healAmount);
        }

        [PunRPC]
        public void ChangePlayerHealth(float healthChange)
        {
            playerHealth -= healthChange;
        }
    }
}