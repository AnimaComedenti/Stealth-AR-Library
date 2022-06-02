using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    public abstract class UITask : MonoBehaviourPun, IInteractable
    {
        [SerializeField] protected GameObject ui;
        [SerializeField] protected KeyCode closeUIButton = KeyCode.Escape;

        public bool isGameCompleted = false;

        public bool isUIOpen = false;
        public abstract void DoingTask();

        public void OnInteract()
        {
            OpenUI();
        }

        public void OpenUI()
        {
            if (ui.activeSelf) return;
            isUIOpen = true;
            ui.SetActive(true);
        }

        public void CloseUI()
        {
            if (Input.GetKey(closeUIButton))
            {
                isUIOpen = false;
                ui.SetActive(false);
            }
        }
    }
}