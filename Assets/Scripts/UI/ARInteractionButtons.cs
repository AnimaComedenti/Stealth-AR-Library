using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StealthDemo
{
    public abstract class ARInteractionButtons : MonoBehaviour
    {
        protected void AddSpritesToImages(Sprite sprite)
        {

            if (sprite != null)
            {
                Image image = transform.GetChild(1).GetComponent<Image>();
                image.sprite = sprite;
            }
            else
            {
                Image image = transform.GetChild(1).GetComponent<Image>();
                image.sprite = null;
            }
        }
    }
}
