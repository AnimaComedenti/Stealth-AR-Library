using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
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
        // Update is called once per frame
        protected virtual void Update()
        {
            SetSkillToActivate(abillityHandler.Abillity);
            HandleCooldowntextField();
        }

        protected void HandleCooldowntextField()
        {
            if (skillToActivate == null && cooldownTextfield.IsActive())
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