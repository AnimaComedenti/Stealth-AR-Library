using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

namespace StealthLib
{
    /*
     * Diese Klasse verarbeitet das Bauen von Objekten
     * Hierbei werden Methoden zur verfügung gestellt welche das Bauen ermöglichen
     * 
     * origin: Die ARSessionOrigin die für das Lösen des Skallierungsproblem genutzt wird.
     * levelMash: Das NavMesh für die Gegner, welche beim platzieren von Objekten aktuallisiert wird.
     */
    public class BuildHandler : MonoBehaviourPun
    {
        [SerializeField] private ARSessionOrigin origin;
        [SerializeField] private NavMeshSurface levelMash;

        /*
         * Diese Methode ermöglicht das Bauen von Objekten.
         * Die Objekte werden im Netzwerk erzeugt, dann das NavMesh aktuallisiert und schlussendlich wird es Scaliert bzw. auf die Richtige entfernung gespawnt.
         * 
         * position: Position an der das Objekt gebaut werden soll
         * obj: Das GameObject welches gebaut werden soll
         * quanternion: Die Rotation die das Object haben solle
         */
        public GameObject SpawnObject(Pose position, GameObject obj, Quaternion quaternion)
        {
            GameObject objToBuild = PhotonNetwork.Instantiate(obj.gameObject.name, position.position, quaternion);
            photonView.RPC("BuildNavMesh", RpcTarget.AllBuffered);
            photonView.RPC("MakeContentStickToOrigin", RpcTarget.AllBuffered, objToBuild.GetComponent<PhotonView>().ViewID);
            return objToBuild;
        }

        /*
         * Methode welche MakeContentAppearAt nutzt um die Objekte in Relation des ARSessionOrigin zu verschieben
         * 
         * objectViewID: Die ID des gespawnten Objektes.
         */
        [PunRPC]
        public void MakeContentStickToOrigin(int objectViewID)
        {
            GameObject objectToStick = PhotonNetwork.GetPhotonView(objectViewID).gameObject;
            origin.MakeContentAppearAt(objectToStick.transform, objectToStick.transform.position, objectToStick.transform.rotation);
        }

        //Aktualisierung des NavMeshes
        [PunRPC]
        public void BuildNavMesh()
        {
            levelMash.BuildNavMesh();
        }
    }
}