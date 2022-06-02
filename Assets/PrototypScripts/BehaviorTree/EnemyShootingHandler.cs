using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthLib;
using Photon.Pun;

namespace StealthDemo
{
    public class EnemyShootingHandler : MonoBehaviour
    {
        [SerializeField] GameObject objectToShootFrom;

        [SerializeField] protected int munition = 80;
        [SerializeField] protected float reloadeCooldown;
        [SerializeField] protected float damage;
        [SerializeField] GameObject bullet;
        [SerializeField] float shootForce;
        [SerializeField] int shootDelay = 1;

        private float currentTime = 0;
        private float armo = 0;
        private float maxShoots;
        private float shootdelay = 0;
        private float shootDelayCount;

        private bool onReloadeCooldown = false;

        private void Start()
        {
            maxShoots = munition;
            armo = maxShoots;
        }

        private void Update()
        {
            SkillUpdate();
        }

        public void OnActivate()
        {
            if (onReloadeCooldown) return;
            if (armo > 0 && shootDelayCount >= shootDelay)
            {
                armo--;
                GameObject spawnedBullet = PhotonNetwork.Instantiate(bullet.name, objectToShootFrom.transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Rigidbody>().AddForce(objectToShootFrom.transform.forward * shootForce, ForceMode.Impulse);
                currentTime = 0;
                shootDelayCount = 0;
            }

            if (armo <= 0)
            {
                armo = maxShoots;
                onReloadeCooldown = true;
            }
        }


        public void SkillUpdate()
        {
            shootDelayCount += Time.deltaTime;

            if (!onReloadeCooldown) return;

            currentTime += Time.deltaTime;
            if (currentTime >= reloadeCooldown)
            {
                onReloadeCooldown = false;
                currentTime = 0;
            }
        }


        #region Getter & Setter
        public int Cooldown { get; private set; }
        public bool HasBeenActivated { get; private set; } = false;
        #endregion
    }
}
