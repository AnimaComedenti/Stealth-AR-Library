using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;
using StealthDemo;

namespace StealthLib
{
    public class EnemyBuildButton : MonoBehaviour
    {
        [SerializeField] BuildableObjectSO _objectToBuild;
        [SerializeField] EnemyPositionButtons enemyPositionButtonsToToggle;
        [SerializeField] GameObject buttonsToToggle;

        private void Start()
        {
            AddSpritesToImages();
        }

        public void OpenEnemyBuildMenu()
        {
            enemyPositionButtonsToToggle.setBuildableObject = _objectToBuild;
            UIToggler.Instance.ToggelUIButtons(buttonsToToggle, enemyPositionButtonsToToggle.gameObject);
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

