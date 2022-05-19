using System.Collections;
using UnityEngine;

namespace StealthDemo
{
    public class SeekerAddItem : MonoBehaviour
    {

        [SerializeField] private Camera _cam;
        [SerializeField] private string itemTag = "Item";
        [SerializeField] private CombatButton[] combatButtons;

        // Update is called once per frame
        void Update()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                AddItemToUI();
            }
        }

        private void AddItemToUI()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchposition = touch.position;
                RaycastHit hit;
                Ray screenCenter = _cam.ScreenPointToRay(touchposition);

                if (Physics.Raycast(screenCenter, out hit))
                {
                    if (!hit.collider.CompareTag(itemTag)) return;

                    foreach (CombatButton combatButton in combatButtons)
                    {
                        if (combatButton.getActivatable() == hit.collider.GetComponent<ItemCollectBehavior>().GetActivatable())
                        {
                            ActivatableObject activatable = combatButton.getActivatable();
                            activatable.SetTimesToUse = activatable.SetTimesToUse++;
                            combatButton.SetActivatableItem(activatable);
                            return;
                        }

                        if (combatButton.getActivatable() == null)
                        {
                            combatButton.SetActivatableItem(hit.collider.GetComponent<ItemCollectBehavior>().GetActivatable());
                            return;
                        }
                    }
                }
            }
        }
    }
}