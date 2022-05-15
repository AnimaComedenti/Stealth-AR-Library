using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthDemo
{
    public class HiderHealthbar : MonoBehaviour
    {
        private Image healthBar;
        private float maxHealth;

        [SerializeField] HiderHealthHandler healthHandler;

        private void Start()
        {
            healthBar = GetComponent<Image>();
            maxHealth = healthHandler.playerHealth;
        }

        private void Update()
        {
            float currentHealth = healthHandler.playerHealth;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
}
