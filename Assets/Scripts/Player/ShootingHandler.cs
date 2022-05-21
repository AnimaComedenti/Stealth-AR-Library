using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace StealthDemo
{
    public class ShootingHandler : ActivatableObject
    {
        [SerializeField] GameObject bullet;
        [SerializeField] float shootForce;
        //Optional if not spawned by network
        [Header("Optional")]
        [SerializeField] GameObject objectToShootFrom;

        private float currentTime = 0;
        private float armo = 0;
        private float maxShoots;
        private float shootdelay = 0;
        private bool onReloadeCooldown = false;

        public void Start()
        {
            maxShoots = abillity.ActivationCount;
            armo = maxShoots;
        }
        public void Update()
        {

            shootdelay += Time.deltaTime;

            if (!onReloadeCooldown) return;

            currentTime += Time.deltaTime;
            if (currentTime >= abillity.Cooldown)
            {
                onReloadeCooldown = false;
                currentTime = 0;
            }
        }
        public override void OnActivate()
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
    }
}
