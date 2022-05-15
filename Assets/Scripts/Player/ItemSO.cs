using UnityEngine;
using System.Collections;

namespace StealthDemo
{

    [CreateAssetMenu(menuName = "ScriptableObjects/ItemSO")]
    public class ItemSO : AbillitySO
    {

        [SerializeField] float itemCount;
        public float ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }
    }
}
