using System.Collections;
using UnityEngine;
using Photon.Pun;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SeekerShooting")]
    public class SeekerShooting : AbillitySO
    {
        [SerializeField] private float radius = 10;
        [SerializeField] private string playerTag;
        [SerializeField] private GameObject soundParticle;

        private Vector3 indicatorPosition;

        public override void OnSkillActivation()
        {
            if (!HasBeenActivated)
            {
                indicatorPosition = SeekerPlacementIndicator.Instance.getPlacementPosition.position;

                PhotonNetwork.Instantiate(soundParticle.name, indicatorPosition, Quaternion.identity);

                Collider[] colliders = Physics.OverlapSphere(indicatorPosition, radius);

                if (colliders.Length > 0)
                {
                    foreach (Collider colider in colliders)
                    {
                        if (colider.CompareTag(playerTag))
                        {
                            colider.gameObject.GetComponent<HiderHealthHandler>().HitPlayer(Damage);
                        }
                    }
                }

                HasBeenActivated = true;
            }
        }
    }
}