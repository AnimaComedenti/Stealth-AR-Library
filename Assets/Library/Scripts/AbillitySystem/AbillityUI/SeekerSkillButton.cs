using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Die Klasse erbt von Skillbutton und ist für das Mappen des Inputes des Smartphone-Spielers zuständig.
     * Hierbei wird eine public OnClick methode zur verfügen gestellt, welche dann händisch in Unity einem Button zugewiesen werden kann.
     */
    public class SeekerSkillButton : SkillButtonWithCooldownText
    {
        //Methode welche den Skill oder das Item ausführt. 
        public void OnClick()
        {
            if (skillToActivate == null) return;
            abillityHandler.OnActivate();
        }
    }
}