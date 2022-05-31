using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    public class HealthbarOutterHandler : MonoBehaviour
    {
        [SerializeField] private HiderHealthHandler healthHandler;
        [SerializeField] private Transform healthBar;
        [SerializeField] private float timeToReavealHealth = 2;

        private float currentHealth;
        private float healthBeforeChange;
        private float timer = 0;
        private bool hasHealthChanged = false;

        void Start()
        {
            currentHealth = healthHandler.playerHealth;
            healthBeforeChange = currentHealth;
        }

        void Update()
        {
            currentHealth = healthHandler.playerHealth;

            if(currentHealth != healthBeforeChange)
            {
                UpdateHealthBar();
                hasHealthChanged = true;
            }

            HandleHealthbarVisable();
        }

        private void UpdateHealthBar()
        {
            Vector3 oldScale = healthBar.localScale;
            healthBar.localScale = new Vector3(currentHealth, oldScale.y, oldScale.z);
            healthBeforeChange = currentHealth;
        }

        private void HandleHealthbarVisable()
        {
            if (hasHealthChanged)
            {
                timer += Time.deltaTime;

                if (timer <= timeToReavealHealth)
                {
                    healthBar.gameObject.SetActive(false);
                    hasHealthChanged = false;
                    timer = 0;
                    return;
                }

                healthBar.gameObject.SetActive(true);
            }
        }
    }
}