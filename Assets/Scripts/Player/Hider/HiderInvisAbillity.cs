using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Abillities/HiderInvisabillity")]
    public class HiderInvisAbillity : AbillitySO, IUpdateableAbillity, IActivatableAbillity
    {
        [SerializeField] private GameObject hider;
        [SerializeField] private LightDetector lightDetector;
        [SerializeField] private float invisAfterActivation = 2f;
        [SerializeField] private float lightLevelToReveal = 10f;
        private PhotonView pv;


        private bool isHiderInvis = false;
        private bool isLightIgnored = true;
        private float timer = 0;

        public void Activate()
        {
            if (!isHiderInvis)
            {
                pv = hider.GetComponent<PhotonView>();
                pv.RPC("ChangeToInvis", RpcTarget.All, true);

                isHiderInvis = true;
                isLightIgnored = true;
            }

        }


        public void SkillUpdate()
        {
            if (isLightIgnored)
            {
                timer += Time.deltaTime;
                if (timer >= invisAfterActivation)
                {
                    timer = 0;
                    isLightIgnored = false;
                }
            }
            else
            {
                if (!isHiderInvis) return;
                if (lightDetector.CurrentLightLevel >= lightLevelToReveal)
                {
                    isHiderInvis = false;

                    pv.RPC("ChangeToInvis", RpcTarget.All, false);
                }
            }
        }
    }
}
