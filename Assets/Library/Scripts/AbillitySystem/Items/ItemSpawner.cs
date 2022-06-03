using System.Collections;
using UnityEngine;

namespace StealthLib
{   
    /*
     * Eine Klasse welche das Spawnen bzw. erzeugen von Items handelt.
     * Diese Klasse nimmt die Seeke- und Hider-Items auf und unterscheidet anhan Endgerät aus welchem der beiden Item-Pools gespawnt werden soll
     * Zusätzlich verfügt diese Klasse über SpawnPositions, einem SpawnChance und TimeInterval.
     * 
     * TimeInterval: Gibt an in welche Interval das Erzeugen der Items überprüft wird.
     * SpawnChance: Gibt an wie hoch die Chance ist das ein Item spawnt.
     * itemSpawnPositions: Dies sind die Positionen auf dennen Items erzeugt werden sollen.
     * seekerItems: Item-Pool des Seekers (Smartphone-Spielers).
     * hiderItems: Item-Pool des Hiders (PC-Spielers).
     */
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] itemSpawnPositions;
        [SerializeField] private GameObject[] seekerItems;
        [SerializeField] private GameObject[] hiderItems;

        //Chance auf Item-Spawn
        [SerializeField] private float spawnChance = 0.25f;
        //Zeitinterval für das erzeugen der Items
        [SerializeField] private float timeIntervalToCheck = 10f;

        private GameObject[] itemsToSpawn;
        private float timer = 0;

        //Toggler um das Erzeugen der Items von außen zu starten/stopen
        public bool ShouldSpawnItems { get; set; } = true;

        void Start()
        {
            //Unterscheidung anhand des Endgeräts, welche Items local gespawnt werden.
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                itemsToSpawn = seekerItems;
            }
            else
            {
                itemsToSpawn = hiderItems;
            }
        }

        void FixedUpdate()
        {
            if (!ShouldSpawnItems) return;
            timer += Time.deltaTime;

            if (timer <= timeIntervalToCheck) return;
            timer = 0;

            SpawnRandomeItem();
        }

        /*
         * Methode die für das Erzeugen der Items zuständig ist.
         * Bestimmt zusätzlich auf welche zufällige Position das Item gespawnt werden soll.
         */
        private void SpawnRandomeItem()
        {
            float choosenNumber = 100 * spawnChance;
            int rndNumber = Random.Range(0, 100);

            if (rndNumber <= choosenNumber)
            {
                int rndIndex = Random.Range(0, itemsToSpawn.Length);
                int rndIndex2 = Random.Range(0, itemSpawnPositions.Length);
                Instantiate(itemsToSpawn[rndIndex], itemSpawnPositions[rndIndex2].position, Quaternion.identity);
            }
        }


        #region Getter & Setter
        public Transform[] ItemSpawnPositions { get => itemSpawnPositions; set => itemSpawnPositions = value; }
        public GameObject[] SeekerItems { get => seekerItems; set => seekerItems = value; }
        public GameObject[] HiderItems { get => hiderItems; set => hiderItems = value; }
        public float SpawnChance { get => spawnChance; set => spawnChance = value; }
        public float TimeIntervalToCheck { get => timeIntervalToCheck; set => timeIntervalToCheck = value; }
        public float CurrentItemspawnTime { get => timer; set => timer = value; }

        #endregion
    }
}