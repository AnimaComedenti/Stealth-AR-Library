using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/HiderInvisAbillity")]
    public class HiderInvisAbillity : AbillitySO, ISkillUpdateable
    {
        [SerializeField] private GameObject hider;
        [SerializeField] private LightDetector lightDetector;
        [SerializeField] private float invisAfterActivation = 2f;
        [SerializeField] private float lightLevelToReveal = 10f;
        [SerializeField] private PhotonView pv;


        private bool isHiderInvis = false;
        private bool isLightIgnored = true;
        private float timer = 0;


        public override void OnSkillActivation()
        {
            if (!isHiderInvis && !HasBeenActivated)
            {
                pv.RPC("ChangeToInvis", RpcTarget.All, true);
                isHiderInvis = true;
                isLightIgnored = true;
                HasBeenActivated = true;
            }
        }

        public  void OnUpdating()
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
                if (lightDetector.currentLightLevel >= lightLevelToReveal)
                {
                    isHiderInvis = false;

                    pv.RPC("ChangeToInvis", RpcTarget.All, false);
                }
            }
        }
    }
}
