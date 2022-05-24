using System.Collections;
using UnityEngine;
using StealthLib;

namespace StealthDemo
{
    public class SeekerAddItem : MonoBehaviour
    {

        [SerializeField] private Camera _cam;
        [SerializeField] private string itemTag = "Item";
        [SerializeField] private SeekerSkillActivationButton[] combatButtons;

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

                    foreach (SeekerSkillActivationButton combatButton in combatButtons)
                    {
                        if (combatButton.getActivatable() == hit.collider.GetComponent<ItemCollectBehavior>().GetActivatable())
                        {
                            AbillitySO activatable = combatButton.getActivatable();
                            activatable.ActivationCount = activatable.ActivationCount++;
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