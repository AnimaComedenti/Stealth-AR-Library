using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Bei dem SkillButton handelt es sich um ein Button welcher die Darstellung der Abillity regelt.
     * Diese Klasse setzt gegebenfalls das Skill-Icon und verarbeitet sowie stellt den Cooldown-Text dar.
     * Die Abillity bekommt diese Klasse durach das konstante lauschen des AbillityHandlers.
     * 
     * imageGameObject: Das Image-Gameobject welches das Icon der Abillity darstellen soll
     * cooldownTextfield: Das Textobjekt zum darstellen des Abillity-Cooldowns
     * abillityHandler: Der AbillityHandler auf den gelauscht wird
     */
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] protected AbillityHandler abillityHandler;
        [SerializeField] protected Image imageGameObject;
        [SerializeField] protected Text cooldownTextfield;

        private AbillitySO skillToActivate;

        protected virtual void Start()
        {
            SetSkillToActivate(abillityHandler.Abillity);
        }
        protected virtual void Update()
        {
            SetSkillToActivate(abillityHandler.Abillity);
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

        public void SetSkillToActivate(AbillitySO skill)
        {
            skillToActivate = skill;

            if(skill == null)
            {
                imageGameObject.sprite = null;
                return;
            }

            imageGameObject.sprite = skill.Icon;
        }

        #region Getter & Setter

        public Image Image { get => imageGameObject; set => imageGameObject = value; }
        public Text CooldownText { get => cooldownTextfield; set => cooldownTextfield = value; }
        public AbillityHandler AbillityHandler { get => abillityHandler; set => abillityHandler = value; }
        public AbillitySO SkillToActivate{ get => skillToActivate; set => skillToActivate = value; }

        #endregion
    }
}