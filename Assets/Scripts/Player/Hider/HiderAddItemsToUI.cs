using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthDemo
{
    public class HiderAddItemsToUI : MonoBehaviour
    {
        [SerializeField] private float pickUpRadius = 0.5f;
        [SerializeField] private GameObject objectToCastFrom;
        [SerializeField] private HiderSkillActivationButton[] skillButtons;

        void Update()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                AddItemToUI();
            }
        }

        private void AddItemToUI()
        {
            Collider[] colliders = Physics.OverlapSphere(objectToCastFrom.transform.position,pickUpRadius);

            if(colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out ItemCollectBehavior itemCollectBehavior))
                    {
                       foreach(HiderSkillActivationButton skillButton in skillButtons)
                        {
                            if (skillButton.getActivatable() == itemCollectBehavior.GetActivatable())
                            {
                                ActivatableObject activatable = skillButton.getActivatable();
                                activatable.SetTimesToUse = activatable.SetTimesToUse++;
                                return;
                            }
                            if (skillButton.getActivatable() == null)
                            {
                                skillButton.SetActivatableItem(collider.GetComponent<ItemCollectBehavior>().GetActivatable());
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}

