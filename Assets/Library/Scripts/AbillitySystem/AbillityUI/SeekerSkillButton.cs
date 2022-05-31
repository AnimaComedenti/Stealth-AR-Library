using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    public class SeekerSkillButton : SkillButton
    {
        public void OnClick()
        {
            abillityHandler.OnActivate();

            if (abillityHandler.CanBeRemoved())
            {
                abillityHandler.Abillity = null;
            }
        }
    }
}