using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthLib;


namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Abillities/PlaceTrap")]
    public class SeekerPlaceTrap: AbillitySO, IActivatableAbillity
    {
        [SerializeField] private GameObject objectToPlace;

        public void Activate()
        {
            PhotonNetwork.Instantiate(objectToPlace.name, SeekerPlacementIndicator.Instance.getPlacementPosition.position, Quaternion.identity);
            ActivationCount--;
        }

    }
}

