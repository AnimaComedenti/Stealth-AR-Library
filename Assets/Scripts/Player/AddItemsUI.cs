using System.Collections;
using UnityEngine;

namespace StealthDemo
{
    public class AddItemsUI : MonoBehaviour
    {
        [SerializeField] private CombatButton[] combatButtons;

        [SerializeField] private string itemTag;

        [SerializeField] private float radius = 10;

        private Vector3 position;


        // Update is called once per frame
        void Update()
        {
            if(SystemInfo.deviceType == DeviceType.Handheld)
            {
                AddItemsToUIHandheld();
                return;
            }

            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                AddItemsToUIDesktop();
                return;
            }
            
        }

        private void AddItemsToUIHandheld()
        {
            position = SeekerPlacementIndicator.Instance.getPlacementPosition.position;

            Collider[] colliders = Physics.OverlapSphere(position, radius);

            if (colliders.Length < 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (!collider.CompareTag(itemTag)) continue;
                    foreach (CombatButton combatButton in combatButtons)
                    {
                        if (combatButton.getActivatable() == null)
                        {
                            combatButton.SetActivatableItem(collider.GetComponent<ItemCollectBehavior>().GetActivatable());
                            return;
                        }
                    }
                }
            }
        }

        private void AddItemsToUIDesktop()
        {
            position = gameObject.transform.position;

            Collider[] colliders = Physics.OverlapSphere(position, radius);

            if (colliders.Length < 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (!collider.CompareTag(itemTag)) continue;
                    foreach (CombatButton combatButton in combatButtons)
                    {
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