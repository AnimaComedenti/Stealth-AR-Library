using System.Collections;
using UnityEngine;

namespace StealthLib
{
    public class RotationBuildButton : BuildButton
    {
        [SerializeField] private RotationButtons rotationButtonsToToggle;
        [SerializeField] private GameObject buildButtonsToToggle;

        /*
         * Methode die an einem Button-OnClick gebunden werden kann.
         * Diese Methode öffnet nach dem Klicken das Rotation-UI.
         */
        public override void BuildOnClick()
        {
            if (rotationButtonsToToggle == null) return;

            rotationButtonsToToggle.SetObjectToBuild(buildableObject);
            UIToggler.Instance.ToggelUIButtons(buildButtonsToToggle, rotationButtonsToToggle.gameObject);
        }
    }
}