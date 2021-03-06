using System.Collections;
using UnityEngine;
using StealthLib;
using UnityEngine.UI;

namespace StealthDemo
{
    public class HealthbarUIHandler : MonoBehaviour
    {
        [SerializeField] HiderHealthHandler healthHandler;
        [SerializeField] Image uiHealthbar;

        private float maxHealth;
        private float currentHealth;

        // Use this for initialization
        void Start()
        {
            maxHealth = healthHandler.playerHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld) return;
            currentHealth = healthHandler.playerHealth;
            uiHealthbar.fillAmount = currentHealth / maxHealth;
        }
    }
}