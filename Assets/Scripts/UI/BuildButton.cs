using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace StealthDemo
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private BuildableObjectSO _objectToBuild;
        [SerializeField] private RotationButtons rotationButtons;
        [SerializeField] private GameObject buildButtons;

        void Start()
        {
            AddSpritesToImages(_objectToBuild.Sprite);
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

        private void AddSpritesToImages(Sprite sprite)
        {
            Image image = transform.GetChild(1).GetComponent<Image>();
            image.sprite = sprite;
        }

    }
}

