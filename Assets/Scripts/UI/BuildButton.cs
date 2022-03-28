using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace StealthARLibrary
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private BuildableObjectSO _objectToBuild;
        [SerializeField] private RotationButtons rotationButtons;
        [SerializeField] private GameObject buildButtons;

        private void Start()
        {
            AddSpritesToImages();
        }

        public void BuildOnClick()
        {
            SeekerPlacementIndicator.Instance.SpawnObject(_objectToBuild.Prefab.gameObject, Quaternion.identity);
        }

        public void RotateBeforeBuild()
        {
            if (rotationButtons == null) return;

            rotationButtons.SetObjectToBuild(_objectToBuild);
            UIToggler.Instance.ToggelUIButtons(buildButtons, rotationButtons.gameObject);
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

