using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthDemo;

namespace StealthLib
{

    /*
     * Diese Klasse ist für das Hören von Objecten zuständig.
     * Diese Klasse gibt bei dem errreichen des Schwellwertes 1 das gehörte Objekt zurück.
     * Zudem berechnet diese Klasse auch den Abfall und Zuwags der Lautstärke je weiter das Object sich befindet.
     * Beachte: Das Object welches belauscht werden soll muss die SoundMakerKlasse implementieren
     * 
     * @maxHearDistance: Die maximale Hördistanz.
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
         * DetectHearingObject ist die Methode welche für das Suchen des Objectes welche Laute macht.
         * @return: Gibt das GameObject zurück welches ein Laut gemacht hat.
         */
        private void DetectHearingObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    //Wenn er sich selbst hört, return
                    if (collider.gameObject.Equals(this.gameObject)) continue;

                    //Überprüfung ob das Gehörte Object über die SoundMaker Klasse verfügt
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
         * Berechnet die Lautstärke der Distanze.
         * Um so näher die Geräuschequelle ist um so größer wird die Laustärke
         * @return: Die berechnete Lautstärke
         */
        private float VolumePerDistance(float distance, float volume)
        {
            return volume * (maxHearDistance - distance) / maxHearDistance;
        }

        /*
         * Überprüft ob das GameoObjekt gehört werden kann und ob dieses schon in der Liste existiert.
         * Falls das Object in der Liste existiert und nicht gehört werden kann wird es entfernt.
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
