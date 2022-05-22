using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StealthDemo
{
    public abstract class SkillButton : MonoBehaviour
    {
        [SerializeField] protected ActivatableObject skillToActivate;
        [SerializeField] protected Text cooldownTextfield;

        protected virtual void Start()
        {
            if(skillToActivate != null) AddSpritesToImages(skillToActivate.GetButtonSprite());
        }
        // Update is called once per frame
        protected virtual void Update()
        {

            if (skillToActivate == null) return;


            if (skillToActivate.hasBeenActivated)
            {
                cooldownTextfield.gameObject.SetActive(true);
                cooldownTextfield.text = skillToActivate.cooldownText + "s";
            }
            else
            {
                cooldownTextfield.gameObject.SetActive(false);
            }


            if (skillToActivate.CanBeRemoved()) SetActivatableItem(null);
        }

        public void SetActivatableItem(ActivatableObject activatable)
        {
            skillToActivate = activatable;

            if (activatable == null)
            {
                AddSpritesToImages(null);
                return;
            }

            AddSpritesToImages(activatable.GetButtonSprite());
            
        }

        protected void AddSpritesToImages(Sprite sprite)
        {
            Image image = transform.GetChild(1).GetComponent<Image>();
            image.sprite = sprite;
        }

        public ActivatableObject getActivatable()
        {
            return skillToActivate;
        }
    }
}

