using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthDemo;

namespace StealthLib
{

    /*
     * Diese Klasse ist f�r das H�ren von Objecten zust�ndig.
     * Diese Klasse gibt bei dem errreichen eines Bestimmten Schwellwertes das geh�rte Objekt zur�ck.
     * Zudem berechnet diese Klasse auch den Abfall der Lautst�rke je weiter das Object sich befindet.
     * Beachte: Das Object welches gelauscht werden soll muss die SoundMakerKlasse implementieren
     * 
     * @maxHearDistance: Die maximale H�rdistanz.
     * @dividerPerDistance: Dividierer die pro Distanz abgezogen werden
     * @maxNoise: Maximaler Schwellenwert der Lautst�rke
     * 
     */
    public class SoundDetector : MonoBehaviour
    {
        [SerializeField] private float maxHearDistance;
        [SerializeField] private float dividerPerDistance = 0.7f;
        [SerializeField] private float maxNoise =1;

        private float distance;
        private float volumePerDistance;
        private float objectVolume;

        public GameObject CurrentHearingObject { get; private set; }
        public float VolumeOfObject {  get { return objectVolume; } private set { objectVolume = value; } }

        void FixedUpdate()
        {
            DetectHearingObject();
        }

        /*
         * DetectHearingObject ist die Methode welche f�r das Suchen des Objectes welche Laute macht.
         * @return: Gibt das GameObject zur�ck welches ein Laut gemacht hat.
         */
        public GameObject DetectHearingObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    //Wenn er sich selbst h�rt, return
                    if (collider.gameObject.CompareTag(gameObject.tag)) continue;

                    //�berpr�fung ob das Geh�rte Object �ber die SoundMaker Klasse verf�gt
                    if (collider.gameObject.TryGetComponent(out SoundMaker soundMaker))
                    {
                        //Distanz errechnen
                        Vector3 makerPosition = soundMaker.transform.position;
                        distance = Vector3.Distance(makerPosition, transform.position);

                        if (soundMaker.Volume() == 0) continue;

                        //Distance je nach Reichweite errechnen
                        objectVolume = VolumePerDistance(distance, soundMaker.Volume(), dividerPerDistance);
                        
                        if (objectVolume >= maxNoise)
                        {
                            CurrentHearingObject = collider.gameObject;
                            return CurrentHearingObject;
                        }
                    }
                }
            }
            CurrentHearingObject = null;
            return CurrentHearingObject;
        }

        /*
         * Berechnet die Lautst�rke der Distanze.
         * Um so n�her die Ger�uschequelle ist um so gr��er wird die Laust�rke
         */
        public float VolumePerDistance(float distance, float volume, float dividerPerDistance)
        {
            float volumePerDistance = dividerPerDistance / distance;
            return volume + volumePerDistance;
        }

    }
}
