using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.save;

namespace InventoryItem
{
    public class PlayerItem : MonoBehaviour, ISaveable
    {
        [SerializeField] int ItemBagSize = 16;

        ItemScriptObject[] slots;

        public event Action InventoryUpdated;
        private void Awake()
        {
            slots = new ItemScriptObject[ItemBagSize];
            slots[0] = ItemScriptObject.GetItemDataFromID("4361f5bd-7e42-425d-9bfe-e325c7ab1a05");
            slots[1] = ItemScriptObject.GetItemDataFromID("f0ce706a-3aea-419b-bc20-6064be7d234e");
            slots[2] = ItemScriptObject.GetItemDataFromID("f0ce706a-3aea-419b-bc20-6064be7d234e");
        }

        public static PlayerItem GetPlayerItem()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<PlayerItem>();
        }

        public ItemScriptObject GetItemInSlot(int slot)
        {
            return slots[slot];
        }

        public int GetItemBagSize()
        {
            return ItemBagSize;
        }

        public bool AddItemToSlot(int slot, ItemScriptObject item)
        {
            if (slots[slot] != null)
            {
                //  return AddToFirstEmptySlot(item); ;
            }

            slots[slot] = item;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
            return true;
        }

        public bool AddToFirstEmptySlot(ItemScriptObject item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i] = item;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
            return true;
        }

        private int FindSlot(ItemScriptObject item)
        {
            return FindEmptySlot();
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveFromSlot(int slot)
        {
            slots[slot] = null;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
        }

        public object CaptureState()
        {
            var slotStrings = new string[ItemBagSize];
            for (int i = 0; i < ItemBagSize; i++)
            {
                if (slots[i] != null)
                {
                    slotStrings[i] = slots[i].GetItemID();
                }
            }
            return slotStrings;
        }

        public void RestoreState(object state)
        {
            var slotStrings = (string[])state;
            for (int i = 0; i < ItemBagSize; i++)
            {
                slots[i] = ItemScriptObject.GetItemDataFromID(slotStrings[i]);
            }

            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
        }
    }
}