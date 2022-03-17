using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthARLibrary
{
    public class HandleUIImages : MonoBehaviour
    {
        [SerializeField] BuildableObjectList buildableObjectList;

        private void Start()
        {
            Transform buildingButtons = transform.Find("BuildButtons");
            buildingButtons.gameObject.SetActive(false);

            BuildableObjectSO level = buildableObjectList.levelprefab;

            int childIndex = 0;
            int buildingIndex = 0;
            foreach (Transform button in buildingButtons)
            {
                if(childIndex != 0)
                {
                    if (buildingIndex > buildableObjectList.objectList.Count-1) return;
                    BuildableObjectSO build = buildableObjectList.objectList[buildingIndex];
                    AddSpritesToImages(button, build);
                    buildingIndex++;
                }
                else
                {
                    AddSpritesToImages(button, level);
                }
                childIndex++;
            }   
        }

        private void AddSpritesToImages(Transform button, BuildableObjectSO building )
        {
            Image image = button.GetChild(1).GetComponent<Image>();
            BuildButton buildButtonScript = button.GetComponent<BuildButton>();
            buildButtonScript.ObjectToSpawn = building;

            if (building.sprite != null)
            {
                image.sprite = building.sprite;
            }
        }
    }
}

