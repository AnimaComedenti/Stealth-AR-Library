using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StealthDemo
{
    public class SeekerSkillActivationButton : SkillButton
    {
        protected Button button;
        protected override void Start()
        {
            base.Start();
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate { OnClick(); });
        }
        public void OnClick()
        {
            activatableObject.OnActivate();

            if (activatableObject.CanBeRemoved())
            {
                SetActivatableItem(null);
            }
        }
    }
}
