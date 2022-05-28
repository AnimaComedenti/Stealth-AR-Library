using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Abillities/StatsBuff")]
    public class HiderStatsBuffItem : Abillity, ISkillUpdateable
    {
        [SerializeField] private GameObject hider;
        [SerializeField] private float speedMultiplyer = 1.5f;
        [SerializeField] private float jumpMultiplyer = 1.5f;
        [SerializeField] private float duration = 5f;

        private float baseMovementSpeed;
        private float basejumpHightSpeed;
        private float baseRunSpeed;
        private float baseSneakSpeed;
        private float durationTimer = 0;
        private bool isActive = false;

        private HiderPlayerController playerController;

        private void MultiplyStats()
        {

            playerController.movementSpeed *= speedMultiplyer;
            playerController.jumpHeight *=  jumpMultiplyer;
            playerController.runSpeed *= speedMultiplyer;
            playerController.sneakSpeed *= speedMultiplyer;
        }

        private void SetDefaultStats()
        {
            playerController.movementSpeed = baseMovementSpeed;
            playerController.jumpHeight = basejumpHightSpeed;
            playerController.runSpeed = baseRunSpeed;
            playerController.sneakSpeed = baseSneakSpeed;
        }

        public override void OnSkillActivation()
        {
            Debug.Log("HiderBuff activated " + HasBeenActivated);
            if (!HasBeenActivated)
            {
                if (isActive) return;

                Debug.Log("HiderBuff loool ");
                playerController = hider.GetComponentInParent<HiderPlayerController>();
                SetDefaultStats();
                MultiplyStats();
                isActive = true;
                HasBeenActivated = true;
                Debug.Log("HiderBuff playercontroller " + isActive);
            }
        }

        public void OnUpdating()
        {
            if (isActive)
            {
                durationTimer += Time.deltaTime;

                if (durationTimer >= duration)
                {
                    SetDefaultStats();
                    isActive = false;
                    activationCount--;
                    durationTimer = 0;
                }
                return;
            }
        }
    }
}