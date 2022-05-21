using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class HiderInvisAbillity : ActivatableObject
    {
        [SerializeField] private GameObject hider;
        [SerializeField] private LightDetector lightDetector;
        [SerializeField] private float invisAfterActivation = 2f;
        [SerializeField] private float lightLevelToReveal = 10f;


        private bool isHiderInvis = false;
        private bool isLightIgnored = true;
        private float timer = 0;
        private PhotonView pv;


        void Start()
        {
            pv = hider.GetComponent<PhotonView>();
        }

        // Update is called once per frame
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (isLightIgnored)
            {
                timer += Time.deltaTime;
                if(timer >= invisAfterActivation)
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

                    pv.RPC("ChangeToInvis", RpcTarget.All,false);
                }
            }
        }

        public override void OnActivate()
        {
            if(!isHiderInvis && !hasBeenActivated)
            {
                pv.RPC("ChangeToInvis", RpcTarget.All, true);
                isHiderInvis = true;
                isLightIgnored = true;
                hasBeenActivated = true;
            }

        }

    }
}
