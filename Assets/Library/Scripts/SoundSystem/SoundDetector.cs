using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthDemo;

namespace StealthLib
{

    /*
     * Diese Klasse ist für das Hören von Objecten zuständig.
     * Diese Klasse gibt bei dem errreichen eines Bestimmten Schwellwertes das gehörte Objekt zurück.
     * Zudem berechnet diese Klasse auch den Abfall der Lautstärke je weiter das Object sich befindet.
     * Beachte: Das Object welches gelauscht werden soll muss die SoundMakerKlasse implementieren
     * 
     * @maxHearDistance: Die maximale Hördistanz.
     * @dividerPerDistance: Dividierer die pro Distanz abgezogen werden
     * @maxNoise: Maximaler Schwellenwert der Lautstärke
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
         * DetectHearingObject ist die Methode welche für das Suchen des Objectes welche Laute macht.
         * @return: Gibt das GameObject zurück welches ein Laut gemacht hat.
         */
        public GameObject DetectHearingObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    //Wenn er sich selbst hört, return
                    if (collider.gameObject.CompareTag(gameObject.tag)) continue;

                    //Überprüfung ob das Gehörte Object über die SoundMaker Klasse verfügt
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
         * Berechnet die Lautstärke der Distanze.
         * Um so näher die Geräuschequelle ist um so größer wird die Laustärke
         */
        public float VolumePerDistance(float distance, float volume, float dividerPerDistance)
        {
            float volumePerDistance = dividerPerDistance / distance;
            return volume + volumePerDistance;
        }

    }
}
