using System.Collections;
using UnityEngine;

namespace StealthDemo
{
    public class HiderStatsBuffItem : ActivatableObject
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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (isActive)
            {
                durationTimer += Time.deltaTime;
                if (durationTimer >= duration)
                {
                    isActive = false;
                    durationTimer = 0;
                }
                return;
            }

            hiderController.movementSpeed = baseMovementSpeed;
            hiderController.jumpHeight = basejumpHightSpeed;
            hiderController.runSpeed = baseRunSpeed;
            hiderController.sneakSpeed = baseSneakSpeed;
        }
        public override void OnActivate()
        {
            if (!hasBeenActivated)
            {
                MultiplyStats();
                hasBeenActivated = true;
                SetTimesToUse = SetTimesToUse--;

                isActive = true;

            }

        }

        private void MultiplyStats()
        {

            hiderController.movementSpeed = baseMovementSpeed * speedMultiplyer;
            hiderController.jumpHeight = basejumpHightSpeed * jumpMultiplyer;
            hiderController.runSpeed = baseRunSpeed * speedMultiplyer;
            hiderController.sneakSpeed = baseSneakSpeed * speedMultiplyer;
        }
    }
}