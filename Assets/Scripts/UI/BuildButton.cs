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
        [SerializeField] private ARUIButtonScript artoggler;
        [SerializeField] private ConfirmRotationButton confirmButton;

        private void Start()
        {
            AddSpritesToImages();
        }

        public void BuildOnClick()
        {
            SeekerPlacementIndicator.Instance.SpawnObject(_objectToBuild.prefab.gameObject, Quaternion.identity);
        }

        public void RotateBeforeBuild()
        {
            if (confirmButton == null) return;

            confirmButton.SetObjectToBuild(_objectToBuild);
            artoggler.ToggelRotationMenu();
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

