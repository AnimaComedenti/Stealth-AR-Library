using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    /*
     * BasisKlasse für eine Task welches über ein UI verfügt
     * 
     * ui: Das UI welches bei Nutzung angezeigt werden soll
     * closeUIButton: Der Button welcher für das Schließen der UI genutzt wird
     */
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

        /*
         * Öffnet das UI
         */
        public void OpenUI()
        {
            if (ui.activeSelf) return;
            isUIOpen = true;
            ui.SetActive(true);
        }

        /*
         * Schließt das UI
         */
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