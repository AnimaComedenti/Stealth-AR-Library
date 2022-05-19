using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StealthDemo
{
    public class HiderSkillActivation : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private KeyCode key;
        [SerializeField] private ActivatableObject skillToActivate;


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(key))
            {
                skillToActivate.OnActivate();
            }
        }
    }

}

