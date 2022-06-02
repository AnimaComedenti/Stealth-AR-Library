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
        [SerializeField] protected float cooldown;
        [SerializeField] protected float damage;
        [SerializeField] GameObject bullet;
        [SerializeField] float shootForce;

        private float currentTime = 0;
        private float armo = 0;
        private float maxShoots;
        private float shootdelay = 0;
        private bool onReloadeCooldown = false;

        protected float cnt = 0;

        private void Start()
        {
            maxShoots = munition;
            armo = maxShoots;
            cnt = cooldown;
        }

        private void FixedUpdate()
        {
            if (HasBeenActivated)
            {
                cnt -= Time.deltaTime;
                if (cnt <= 0)
                {
                    cnt = cooldown;
                    HasBeenActivated = false;
                }
                Cooldown = (int)cnt;
            }
            SkillUpdate();
        }

        public void OnActivate()
        {
            if (HasBeenActivated) return;
            Activate();
            HasBeenActivated = true;
        }


        public void SkillUpdate()
        {
            shootdelay += Time.deltaTime;

            if (!onReloadeCooldown) return;

            currentTime += Time.deltaTime;
            if (currentTime >= cooldown)
            {
                onReloadeCooldown = false;
                currentTime = 0;
            }
        }

        public void Activate()
        {
            if (onReloadeCooldown) return;
            if (armo > 0 && shootdelay >= 1)
            {
                armo--;
                GameObject spawnedBullet = PhotonNetwork.Instantiate(bullet.name, objectToShootFrom.transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Rigidbody>().AddForce(objectToShootFrom.transform.forward * shootForce, ForceMode.Impulse);
                currentTime = 0;
                shootdelay = 0;
            }

            if (armo <= 0)
            {
                armo = maxShoots;
                onReloadeCooldown = true;
            }
        }

        #region Getter & Setter
        public int Cooldown { get; private set; }
        public bool HasBeenActivated { get; private set; } = false;
        #endregion
    }
}
