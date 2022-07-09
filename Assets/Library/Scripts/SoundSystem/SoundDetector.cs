using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthDemo;

namespace StealthLib
{

    /*
     * Diese Klasse ist für das Hören von Objecten zuständig.
     * Diese Klasse gibt bei dem errreichen des Schwellwertes 1 das gehörte Objekt zurück.
     * Zudem berechnet diese Klasse auch den Abfall und Zuwags der Lautstärke je weiter das Object sich von diesem befindet.
     * Beachte: Das Object welches belauscht werden soll muss die SoundMakerKlasse implementieren
     * 
     * @maxHearDistance: Die maximale Hördistanz.
     * 
     */
    public class SoundDetector : MonoBehaviour
    {
        [SerializeField] private float maxHearDistance;

        //SoundLevel ab dem ein Geräusch gehört wird. Muss zischen 0 und 1 Liegen. Diser wert sollte so groß sein wie die minimale Lautstärke die gehört werden soll.
        //Bsp. Soll das schleichen eines Spielers gehört werden, dessen Sound 0.6 beträgt, so sollte dieser wert 0.5 sein
        [SerializeField] private float soundLevelToHear = 0.5f;

        private float distance;
        private float objectVolume;
        public List<GameObject> CurrentHearingObjects { get; private set; } = new List<GameObject>();

        void FixedUpdate()
        {
            DetectHearingObject();
        }

        /*
         * DetectHearingObject ist die Methode welche für das Hören eines Objektes zuständig ist.
         * @return: Gibt das GameObject zurück welches ein Laut gemacht hat.
         */
        private void DetectHearingObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    //Wenn er sich selbst hört, continue
                    if (collider.gameObject.Equals(this.gameObject)) continue;

                    //Überprüfung ob das Gehörte Object über die SoundMaker Klasse verfügt
                    if (collider.gameObject.TryGetComponent(out SoundMaker soundMaker))
                    {
                        //Distanz errechnen
                        Vector3 makerPosition = soundMaker.transform.position;
                        distance = Vector3.Distance(makerPosition, transform.position);

                        //Sound je nach Reichweite errechnen
                        objectVolume = VolumePerDistance(distance, soundMaker.Volume);

                        AddHearedObjectToList(collider.gameObject);
                    }
                }
            }
        }

        /*
         * Berechnet die Lautstärke der Distanze.
         * Die Lautstärke wird hierbei je nach größe der Distanz verringert. Ab einer Distanz von 0 ist die Lautstärke so laut wie ihr default Wert und zwar 100%.
         * Bsp. Lautstärke von 0.2 ist bei einer Distanz von 0 genau 0.2 Laut, ist die Distanz größer als 0 so ist die Lautstärke < 0.2.
         * @return: Die berechnete Lautstärke
         */
        public float VolumePerDistance(float distance, float volume)
        {
            return  volume * (maxHearDistance - distance) / maxHearDistance;
        }

        /*
         * Überprüft ob das GameoObjekt gehört werden kann und ob dieses schon in der Liste existiert.
         * Falls das Object in der Liste existiert und nicht gehört werden kann wird es entfernt.
         */
        private void AddHearedObjectToList(GameObject gameObject)
        {
            if(objectVolume >= soundLevelToHear && CurrentHearingObjects.Count < 0)
            {
                CurrentHearingObjects.Add(gameObject);
                return;
            }

            foreach(GameObject obj in CurrentHearingObjects)
            {
                if (obj.Equals(gameObject))
                {
                    if(objectVolume >= soundLevelToHear) return;

                    CurrentHearingObjects.Remove(obj);
                    return;
                } 
            }

            if(objectVolume >= soundLevelToHear) CurrentHearingObjects.Add(gameObject);
        }

    }
}
