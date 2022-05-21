using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthDemo
{
    public class HiderSkillActivationButton : SkillButton
    {
        // Start is called before the first frame update

        [SerializeField] private KeyCode key;

        // Update is called once per frame
        protected override void  Update()
        {
            base.Update();

            if (Input.GetKey(key))
            {
                skillToActivate.OnActivate();
            }

        }
    }

}

