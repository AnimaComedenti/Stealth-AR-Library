using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StealthDemo
{
    public class CombatButton : ARInteractionButtons
    {
        [SerializeField] private ActivatableObject activatable;
        [SerializeField] private Text cooldownTextfield;

        private Button button;
        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate { OnClick(); });

            AddSpritesToImages(activatable.GetButtonSprite());
        }

        private void FixedUpdate()
        {
            if (activatable == null) return;

            if (activatable.hasBeenActivated)
            {
                cooldownTextfield.gameObject.SetActive(true);
                cooldownTextfield.text = activatable.cooldownText + "s";
            }
            else
            {
                cooldownTextfield.gameObject.SetActive(false);
            }
        }

        public void OnClick()
        {
            ActivatableObject activatable1 =  activatable.OnActivate();
            if (activatable1 == null) SetActivatableItem(activatable);
        }

        public void SetActivatableItem(ActivatableObject activatable)
        {
            AddSpritesToImages(activatable.GetButtonSprite());
            this.activatable = activatable;
        }

        public ActivatableObject getActivatable()
        {
            return activatable;
        }
    }
}
