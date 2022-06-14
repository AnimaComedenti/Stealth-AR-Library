using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Bei dem SkillButton handelt es sich um ein Button welcher die Darstellung der Abillity regelt.
     * Diese Klasse setzt gegebenfalls das Skill-Icon und verarbeitet sowie stellt den Cooldown-Text dar.
     * Die Abillity bekommt diese Klasse durach das konstante lauschen des AbillityHandlers.
     * 
     * imageGameObject: Das Image-Gameobject welches das Icon der Abillity darstellen soll
     * cooldownTextfield: Das Textobjekt zum darstellen des Abillity-Cooldowns
     * abillityHandler: Der AbillityHandler auf den gelauscht wird
     */
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] protected AbillityHandler abillityHandler;
        [SerializeField] protected Image imageGameObject;

        protected AbillitySO skillToActivate;

        protected virtual void Start()
        {
            SetSkillToActivate(abillityHandler.Abillity);
        }
        protected virtual void Update()
        {
            SetSkillToActivate(abillityHandler.Abillity);
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
        public AbillityHandler AbillityHandler { get => abillityHandler; set => abillityHandler = value; }
        public AbillitySO SkillToActivate{ get => skillToActivate; set => skillToActivate = value; }

        #endregion
    }
}