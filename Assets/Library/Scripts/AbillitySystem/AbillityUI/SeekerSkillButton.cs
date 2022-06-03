using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Die Klasse erbt von Skillbutton und ist für das Mappen des Inputes des Smartphone-Spielers zuständig.
     * Hierbei wird eine public OnClick methode zur verfügen gestellt, welche dann händisch in Unity einem Button zugewiesen werden kann.
     */
    public class SeekerSkillButton : SkillButton
    {
        //Methode welche den Skill oder das Item ausführt. 
        public void OnClick()
        {
            abillityHandler.OnActivate();

           /*
            * Abfrage ob die Abillity, welche auf dem Handler sowie auf dem Button sitzt, entfernt werden kann.
            */
            if (abillityHandler.CanBeRemoved())
            {
                abillityHandler.Abillity = null;
            }
        }
    }
}