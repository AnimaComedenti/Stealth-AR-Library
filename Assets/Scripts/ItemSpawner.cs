using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] itemSpawnPositions;
        [SerializeField] private GameObject[] seekerItems;
        [SerializeField] private GameObject[] hiderItems;

        [SerializeField] private float spawnChance = 0.25f;
        [SerializeField] private float timeIntervalToCheck = 10f;

        private GameObject[] itemsToSpawn;
        private float timer = 0;
        // Start is called before the first frame update
        void Start()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                itemsToSpawn = seekerItems;
            }
            else
            {
                itemsToSpawn = hiderItems;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!SeekerPlacementIndicator.Instance.isLevelPlaced) return;

            timer += Time.deltaTime;

            if (timer <= timeIntervalToCheck) return;

            timer = 0;

            float choosenNumber = 100 * spawnChance;
            int rndNumber = Random.Range(0, 100);

            if(rndNumber <= choosenNumber)
            {
                int rndIndex = Random.Range(0, itemsToSpawn.Length);
                int rndIndex2 = Random.Range(0, itemSpawnPositions.Length);
                Instantiate(itemsToSpawn[rndIndex], itemSpawnPositions[rndIndex2].position, Quaternion.identity);
            }
        }
    }
}

