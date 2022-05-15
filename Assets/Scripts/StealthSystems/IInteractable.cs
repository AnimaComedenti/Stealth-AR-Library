using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StealthDemo
{
    interface IInteractable
    {
        public void OnInteract(GameObject player);
    }
}

