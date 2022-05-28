using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] protected AbillityHandler abillityHandler;
        [SerializeField] protected Image image;
        [SerializeField] protected Text cooldownTextfield;

        private AbillitySO skillToActivate;

        protected virtual void Start()
        {
            SetActivatableItem(abillityHandler.Abillity);
        }
        // Update is called once per frame
        protected virtual void Update()
        {
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

            cooldownTextfield.gameObject.SetActive(true);
            cooldownTextfield.text = abillityHandler.Cooldown + "s";
        }

        public void SetActivatableItem(AbillitySO activatable)
        {
            if (skillToActivate == null && activatable == null) return;

            skillToActivate = activatable;
            abillityHandler.Abillity = activatable;
            image.sprite = activatable.Icon;

        }

        #region Getter & Setter

        public Image Image { get => image; set => image = value; }
        public Text CooldownText { get => cooldownTextfield; set => cooldownTextfield = value; }
        public AbillityHandler AbillityHandler { get => abillityHandler; set => abillityHandler = value; }
        public AbillitySO SkillToActivate{ get => skillToActivate; set => skillToActivate = value; }

        #endregion
    }
}