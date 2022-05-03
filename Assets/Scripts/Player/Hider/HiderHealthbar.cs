using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HiderHealthbar : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    [SerializeField] private float maxHealth;

    [SerializeField] HiderHealthHandler healthHandler;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        maxHealth = currentHealth;
    }

    private void Update()
    {
        currentHealth = healthHandler.playerHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
