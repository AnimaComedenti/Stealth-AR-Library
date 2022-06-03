using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StealthLib
{
    /*
     * Interface welche für das Interagieren mit dem Hider implementiert werden kann
     */
    interface IInteractable
    {
        public void OnInteract();
    }
}

