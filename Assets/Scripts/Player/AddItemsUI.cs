using System.Collections;
using UnityEngine;

namespace StealthDemo
{
    public class AddItemsUI : MonoBehaviour
    {
        [SerializeField] private CombatButton[] combatButtons;

        [SerializeField] private string itemTag = "Item";

        [SerializeField] private float radius = 10;

        private Vector3 position;


        // Update is called once per frame
        void Update()
        {
            if(SystemInfo.deviceType == DeviceType.Desktop)
            {
                position = gameObject.transform.position;
                AddItemsToUI(position);
            }
           
        }


        private void AddItemsToUI(Vector3 positionToCastFrom)
        {
            Collider[] colliders = Physics.OverlapSphere(position, radius);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (!collider.CompareTag(itemTag)) continue;
                    foreach (CombatButton combatButton in combatButtons)
                    {
                        if (combatButton.getActivatable() == collider.GetComponent<ItemCollectBehavior>().GetActivatable())
                        {
                            ActivatableObject activatable = combatButton.getActivatable();
                            activatable.SetTimesToUse = activatable.SetTimesToUse++;
                            combatButton.SetActivatableItem(activatable);
                            return;
                        }

                        if (combatButton.getActivatable() == null)
                        {
                            combatButton.SetActivatableItem(collider.GetComponent<ItemCollectBehavior>().GetActivatable());
                            return;
                        }
                    }
                }
            }
        }
    }
}