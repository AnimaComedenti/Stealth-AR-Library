using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthLib;


namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SeekerPlaceTrap")]
    public class SeekerPlaceTrap: AbillitySO
    {
        [SerializeField] private GameObject objectToPlace;

        public override void OnSkillActivation()
        {
            if (!HasBeenActivated)
            {
                PhotonNetwork.Instantiate(objectToPlace.name, SeekerPlacementIndicator.Instance.getPlacementPosition.position, Quaternion.identity);

                HasBeenActivated = true;
                activationCount--;
            }
        }
    }
}

