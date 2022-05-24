using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using StealthLib;

namespace StealthDemo
{
    public abstract class SkillButton : MonoBehaviour
    {
        [SerializeField] protected ActivatableObject activatableObject;
        [SerializeField] protected Text cooldownTextfield;

        protected AbillitySO skillToActivate;

        protected virtual void Start()
        {
            skillToActivate = activatableObject.abillity;
            if (skillToActivate != null) AddSpritesToImages(skillToActivate.Icon);
        }
        // Update is called once per frame
        protected virtual void Update()
        {

            if (skillToActivate == null)
            {
                cooldownTextfield.gameObject.SetActive(false);
                return;
            }

            if (activatableObject.CanBeRemoved())
            {
                SetActivatableItem(null);
            }


            if (skillToActivate.HasBeenActivated)
            {
                cooldownTextfield.gameObject.SetActive(true);
                cooldownTextfield.text = activatableObject.cooldownText + "s";
            }
            else
            {
                cooldownTextfield.gameObject.SetActive(false);
            }

        }

        public void SetActivatableItem(AbillitySO activatable)
        {
            skillToActivate = activatable;
            activatableObject.abillity = activatable;
            Debug.Log("Item setted "+activatable);

            if (activatable == null)
            {
                AddSpritesToImages(null);
                return;
            }

            AddSpritesToImages(activatable.Icon);
            
        }

        protected void AddSpritesToImages(Sprite sprite)
        {
            Image image = transform.GetChild(1).GetComponent<Image>();
            image.sprite = sprite;
        }

        public AbillitySO getActivatable()
        {
            return skillToActivate;
        }
    }
}

