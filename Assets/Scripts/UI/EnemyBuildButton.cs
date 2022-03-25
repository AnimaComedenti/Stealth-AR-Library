using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthARLibrary
{
    public class EnemyBuildButton : MonoBehaviour
    {
        [SerializeField] BuildableObjectSO _objectToBuild;
        [SerializeField] ARUIButtonScript aRUIButton;
        [SerializeField] AiBuildButtons[] aiBuildButtons;

        private void Start()
        {
            AddSpritesToImages();
        }

        public void BuildOnClick()
        {
            foreach(AiBuildButtons button in aiBuildButtons)
            {
                button.setBuildableObject = _objectToBuild;
            }
            aRUIButton.ToggelAiUIButtons();
        }

        private void AddSpritesToImages()
        {
            Image image = transform.GetChild(1).GetComponent<Image>();

            if (_objectToBuild.sprite != null)
            {
                image.sprite = _objectToBuild.sprite;
            }
        }
    }
}

