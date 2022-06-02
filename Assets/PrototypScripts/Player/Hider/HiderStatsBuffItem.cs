using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Abillities/StatsBuff")]
    public class HiderStatsBuffItem : AbillitySO, IUpdateableAbillity, IActivatableAbillity
    {
        [SerializeField] private float speedMultiplyer = 1.5f;
        [SerializeField] private float jumpMultiplyer = 1.5f;
        [SerializeField] private float duration = 15f;

        private float baseMovementSpeed;
        private float basejumpHightSpeed;
        private float baseRunSpeed;
        private float baseSneakSpeed;
        private float durationTimer = 0;
        private bool isActive = false;
        private bool firstTimeActive = true;

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
            baseMovementSpeed = playerController.movementSpeed;
            basejumpHightSpeed = playerController.jumpHeight;
            baseRunSpeed = playerController.runSpeed;
            baseSneakSpeed = playerController.sneakSpeed;
        }


        private void ResetValues()
        {
           playerController.movementSpeed = baseMovementSpeed;
           playerController.jumpHeight = basejumpHightSpeed;
           playerController.runSpeed = baseRunSpeed;
           playerController.sneakSpeed = baseSneakSpeed;
        }
        public void SkillUpdate()
        {
            if (isActive)
            {
                durationTimer += Time.deltaTime;

                if (durationTimer >= duration)
                {
                    ResetValues();
                    isActive = false;
                    durationTimer = 0;
                }
                return;
            }
        }

        public void Activate()
        {
            if (playerController == null)
            {
                playerController = FindObjectOfType<HiderPlayerController>();
            }

            if (firstTimeActive)
            {
                SetDefaultStats();
                firstTimeActive = false;
            }

            ResetValues();
            MultiplyStats();
            durationTimer = 0;
            isActive = true;
         }
    }
}