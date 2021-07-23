using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItem
{
    [RequireComponent(typeof(Pickup))]
    public class ClickPickup : MonoBehaviour
    {
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        public bool ClickItem()
        {
            pickup.PickItem();
            return true;
        }
    }
}
