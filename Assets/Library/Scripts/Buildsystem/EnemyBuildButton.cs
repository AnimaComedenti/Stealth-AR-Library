using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;
using StealthDemo;

namespace StealthLib
{
    /*
     * Eigenständige Klasse welches sich auf das bauen der Gegnerobjekte befasst
     * 
     * enemyPositionButtonsToToggle: Die EnemyPosition buttons auf die weitergeleitet wird
     * buttonsToToggle: Die Buttons die getoggelt werden sollen
     */
    public class EnemyBuildButton : BuildButton
    {
        [SerializeField] private EnemyPositionButtons enemyPositionButtonsToToggle;
        [SerializeField] private GameObject buttonsToToggle;


        public override void BuildOnClick()
        {
            enemyPositionButtonsToToggle.setBuildableObject = buildableObject;
            UIToggler.Instance.ToggelUIButtons(buttonsToToggle, enemyPositionButtonsToToggle.gameObject);
        }
    }
}

