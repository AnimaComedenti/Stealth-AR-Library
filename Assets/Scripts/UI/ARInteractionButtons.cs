using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ARInteractionButtons : MonoBehaviour
{
    protected void AddSpritesToImages(Sprite sprite)
    {
            
        if (sprite != null)
        {
            Image image = transform.GetChild(1).GetComponent<Image>();
            image.sprite = sprite;
        }
    }
}
