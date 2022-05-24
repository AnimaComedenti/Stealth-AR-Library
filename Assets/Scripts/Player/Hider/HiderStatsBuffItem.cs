using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    [CreateAssetMenu(menuName = "ScriptableObjects/HiderStatsBuffItem")]
    public class HiderStatsBuffItem : AbillitySO, ISkillUpdateable
    {
        [SerializeField] private HiderPlayerController hiderController;
        [SerializeField] private float speedMultiplyer = 1.5f;
        [SerializeField] private float jumpMultiplyer = 1.5f;
        [SerializeField] private float duration = 5f;

        private float baseMovementSpeed;
        private float basejumpHightSpeed;
        private float baseRunSpeed;
        private float baseSneakSpeed;
        private float durationTimer = 0;
        private bool isActive = false;

        private void Start()
        {
            baseMovementSpeed = hiderController.movementSpeed;
            basejumpHightSpeed = hiderController.jumpHeight;
            baseRunSpeed = hiderController.runSpeed;
            baseSneakSpeed = hiderController.sneakSpeed;
        }

        private void MultiplyStats()
        {

            hiderController.movementSpeed = baseMovementSpeed * speedMultiplyer;
            hiderController.jumpHeight = basejumpHightSpeed * jumpMultiplyer;
            hiderController.runSpeed = baseRunSpeed * speedMultiplyer;
            hiderController.sneakSpeed = baseSneakSpeed * speedMultiplyer;
        }

        private void SetDefaultStats()
        {
            hiderController.movementSpeed = baseMovementSpeed;
            hiderController.jumpHeight = basejumpHightSpeed;
            hiderController.runSpeed = baseRunSpeed;
            hiderController.sneakSpeed = baseSneakSpeed;
        }

        public override void OnSkillActivation()
        {
            if (!HasBeenActivated)
            {
                MultiplyStats();
                activationCount--;
                isActive = true;
            }
        }

        public void OnUpdating()
        {
            if (isActive)
            {
                durationTimer += Time.deltaTime;
                if (durationTimer >= duration)
                {
                    isActive = false;
                    durationTimer = 0;
                    SetDefaultStats();
                }
                return;
            }
        }
    }
}