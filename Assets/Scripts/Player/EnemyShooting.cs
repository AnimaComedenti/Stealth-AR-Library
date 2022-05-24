using UnityEngine;
using System.Collections;
using Photon.Pun;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyShooting")]
    public class EnemyShooting : AbillitySO, ISkillUpdateable
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
            maxShoots = ActivationCount;
            armo = maxShoots;
        }

        public override void OnSkillActivation()
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

        public void OnUpdating()
        {
            shootdelay += Time.deltaTime;

            if (!onReloadeCooldown) return;

            currentTime += Time.deltaTime;
            if (currentTime >= Cooldown)
            {
                onReloadeCooldown = false;
                currentTime = 0;
            }
        }
    }
}
