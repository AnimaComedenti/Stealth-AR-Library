using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthDemo;

namespace StealthLib
{

    /*
     * Diese Klasse ist f�r das H�ren von Objecten zust�ndig.
     * Diese Klasse gibt bei dem errreichen des Schwellwertes 1 das geh�rte Objekt zur�ck.
     * Zudem berechnet diese Klasse auch den Abfall und Zuwags der Lautst�rke je weiter das Object sich von diesem befindet.
     * Beachte: Das Object welches belauscht werden soll muss die SoundMakerKlasse implementieren
     * 
     * @maxHearDistance: Die maximale H�rdistanz.
     * 
     */
    public class SoundDetector : MonoBehaviour
    {
        [SerializeField] private float maxHearDistance;

        //SoundLevel ab dem ein Ger�usch geh�rt wird. Muss zischen 0 und 1 Liegen. Diser wert sollte so gro� sein wie die minimale Lautst�rke die geh�rt werden soll.
        //Bsp. Soll das schleichen eines Spielers geh�rt werden, dessen Sound 0.6 betr�gt, so sollte dieser wert 0.5 sein
        [SerializeField] private float soundLevelToHear = 0.5f;

        private float distance;
        private float objectVolume;
        public List<GameObject> CurrentHearingObjects { get; private set; } = new List<GameObject>();

        void FixedUpdate()
        {
            DetectHearingObject();
        }

        /*
         * DetectHearingObject ist die Methode welche f�r das H�ren eines Objektes zust�ndig ist.
         * @return: Gibt das GameObject zur�ck welches ein Laut gemacht hat.
         */
        private void DetectHearingObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxHearDistance);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    //Wenn er sich selbst h�rt, continue
                    if (collider.gameObject.Equals(this.gameObject)) continue;

                    //�berpr�fung ob das Geh�rte Object �ber die SoundMaker Klasse verf�gt
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
         * Berechnet die Lautst�rke der Distanze.
         * Die Lautst�rke wird hierbei je nach gr��e der Distanz verringert. Ab einer Distanz von 0 ist die Lautst�rke so laut wie ihr default Wert und zwar 100%.
         * Bsp. Lautst�rke von 0.2 ist bei einer Distanz von 0 genau 0.2 Laut, ist die Distanz gr��er als 0 so ist die Lautst�rke < 0.2.
         * @return: Die berechnete Lautst�rke
         */
        public float VolumePerDistance(float distance, float volume)
        {
            return  volume * (maxHearDistance - distance) / maxHearDistance;
        }

        /*
         * �berpr�ft ob das GameoObjekt geh�rt werden kann und ob dieses schon in der Liste existiert.
         * Falls das Object in der Liste existiert und nicht geh�rt werden kann wird es entfernt.
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
