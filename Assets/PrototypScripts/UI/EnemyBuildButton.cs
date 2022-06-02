using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;

namespace StealthDemo
{
    public class EnemyBuildButton : MonoBehaviour
    {
        [SerializeField] BuildableObjectSO _objectToBuild;
        [SerializeField] AiBuildButtons aiBuildButtons;
        [SerializeField] GameObject buildButtons;

        private void Start()
        {
            AddSpritesToImages();
        }

        public void BuildOnClick()
        { 
            aiBuildButtons.setBuildableObject = _objectToBuild;
            UIToggler.Instance.ToggelUIButtons(buildButtons, aiBuildButtons.gameObject);
        }

        private void AddSpritesToImages()
        {
            Image image = transform.GetChild(1).GetComponent<Image>();

            if (_objectToBuild.Sprite != null)
            {
                image.sprite = _objectToBuild.Sprite;
            }
        }
    }
}

