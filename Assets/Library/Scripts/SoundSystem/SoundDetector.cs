using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthDemo;

namespace StealthLib
{

    /*
     * Diese Klasse ist f�r das H�ren von Objecten zust�ndig.
     * Diese Klasse gibt bei dem errreichen des Schwellwertes 1 das geh�rte Objekt zur�ck.
     * Zudem berechnet diese Klasse auch den Abfall und Zuwags der Lautst�rke je weiter das Object sich befindet.
     * Beachte: Das Object welches belauscht werden soll muss die SoundMakerKlasse implementieren
     * 
     * @maxHearDistance: Die maximale H�rdistanz.
     * 
     */
    public class SoundDetector : MonoBehaviour
    {
        [SerializeField] private float maxHearDistance;

        private float distance;
        private float objectVolume;
        public List<GameObject> CurrentHearingObjects { get; private set; }

        void FixedUpdate()
        {
            DetectHearingObject();
        }

        /*
         * DetectHearingObject ist die Methode welche f�r das Suchen des Objectes welche Laute macht.
         * @return: Gibt das GameObject zur�ck welches ein Laut gemacht hat.
         */
        private void DetectHearingObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    //Wenn er sich selbst h�rt, return
                    if (collider.gameObject.Equals(this.gameObject)) continue;

                    //�berpr�fung ob das Geh�rte Object �ber die SoundMaker Klasse verf�gt
                    if (collider.gameObject.TryGetComponent(out SoundMaker soundMaker))
                    {
                        //Distanz errechnen
                        Vector3 makerPosition = soundMaker.transform.position;
                        distance = Vector3.Distance(makerPosition, transform.position);

                        //Distance je nach Reichweite errechnen
                        objectVolume = VolumePerDistance(distance, soundMaker.Volume);

                        AddHearedObjectToList(collider.gameObject);
                    }
                }
            }
        }

        /*
         * Berechnet die Lautst�rke der Distanze.
         * Um so n�her die Ger�uschequelle ist um so gr��er wird die Laust�rke
         * @return: Die berechnete Lautst�rke
         */
        private float VolumePerDistance(float distance, float volume)
        {
            return volume * (maxHearDistance - distance) / maxHearDistance;
        }

        /*
         * �berpr�ft ob das GameoObjekt geh�rt werden kann und ob dieses schon in der Liste existiert.
         * Falls das Object in der Liste existiert und nicht geh�rt werden kann wird es entfernt.
         */
        private void AddHearedObjectToList(GameObject gameObject)
        {
            foreach(GameObject obj in CurrentHearingObjects)
            {
                if (obj.Equals(gameObject))
                {
                    if(objectVolume >= 1) return;

                    CurrentHearingObjects.Remove(obj);
                    return;
                } 
            }

            if(objectVolume >= 1) CurrentHearingObjects.Add(gameObject);
        }

    }
}
