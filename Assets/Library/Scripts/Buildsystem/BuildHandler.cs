using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

namespace StealthLib
{
    public class BuildHandler : MonoBehaviourPun
    {
        [SerializeField] private ARSessionOrigin origin;
        [SerializeField] private NavMeshSurface levelMash;

        public GameObject SpawnObject(Pose position, GameObject obj, Quaternion quaternion)
        {
            GameObject objToBuild = PhotonNetwork.Instantiate(obj.gameObject.name, position.position, quaternion);
            photonView.RPC("BuildNavMesh", RpcTarget.AllBuffered);
            photonView.RPC("MakeContentStickToOrigin", RpcTarget.AllBuffered, objToBuild.GetComponent<PhotonView>().ViewID);
            return objToBuild;
        }

        [PunRPC]
        public void MakeContentStickToOrigin(int objectViewID)
        {
            GameObject objectToStick = PhotonNetwork.GetPhotonView(objectViewID).gameObject;
            origin.MakeContentAppearAt(objectToStick.transform, objectToStick.transform.position, objectToStick.transform.rotation);
        }

        [PunRPC]
        public void BuildNavMesh()
        {
            levelMash.BuildNavMesh();
        }
    }
}