using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItem
{
    public class Pickup : MonoBehaviour
    {
        ItemScriptObject item;
        int number;

        PlayerItem playerItem;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerItem = player.GetComponent<PlayerItem>();
        }

        public void Setup(ItemScriptObject item,int number)
        {
            this.item = item;
            this.number = number;
        }

        public ItemScriptObject GetItem()
        {
            return item;
        }
        public int GetNumber()
        {
            return number;
        }

        public void PickItem()
        {
            bool foundSlot = playerItem.AddToFirstEmptySlot(item, number);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }

        //FOR CURSOR
        public bool CanBePickup()
        {
            return playerItem.HasSpaceFor(item);
        }
    }
}

