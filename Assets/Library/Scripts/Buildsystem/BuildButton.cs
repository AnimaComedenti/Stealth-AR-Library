using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StealthLib
{
    /*
     * Bei dieser Klasse handelt es sich um eine abstrakte Klasse für die BuildButtons.
     * 
     * buildableObject: das Object welches gebaut werden soll
     */
    public abstract class BuildButton : MonoBehaviour
    {
        [SerializeField] protected BuildableObjectSO buildableObject;
        [SerializeField] protected Image ImageObject;
        protected virtual void Start()
        {
            AddSpritesToImage(buildableObject.Sprite);
        }
        public abstract void BuildOnClick();

        /*
         * Methode für das Darstellen des BuildableObjectIcons
         */
        public void AddSpritesToImage(Sprite sprite)
        {
            ImageObject.sprite = sprite;
        }

        public BuildableObjectSO BuildableObjct
        {
            get => buildableObject;
            set
            {
                buildableObject = value;
                AddSpritesToImage(buildableObject.Sprite);
            }
        }
    }
}