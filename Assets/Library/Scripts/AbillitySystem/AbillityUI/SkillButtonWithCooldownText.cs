using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    public class SkillButtonWithCooldownText : SkillButton
    {
        [SerializeField] protected Text cooldownTextfield;

        // Update is called once per frame
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            HandleCooldowntextField();
        }

        //Verarbeitung und Darstellung des Cooldown-Textes
        protected void HandleCooldowntextField()
        {
            if (skillToActivate == null)
            {
                cooldownTextfield.gameObject.SetActive(false);
                return;
            }

            if (!abillityHandler.HasBeenActivated)
            {
                cooldownTextfield.gameObject.SetActive(false);
                return;
            }
            else
            {
                cooldownTextfield.gameObject.SetActive(true);
                cooldownTextfield.text = abillityHandler.Cooldown + "s";
            }
        }
        public Text CooldownText { get => cooldownTextfield; set => cooldownTextfield = value; }
    }
}