using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class SeekerPlaceTrap: ActivatableObject
    {
        [SerializeField] private GameObject objectToPlace;

        public override void OnActivate()
        {
            if (!hasBeenActivated)
            {
                PhotonNetwork.Instantiate(objectToPlace.name, SeekerPlacementIndicator.Instance.getPlacementPosition.position, Quaternion.identity);

                hasBeenActivated = true;
                SetTimesToUse = SetTimesToUse--;
            }
        }
    }
}

