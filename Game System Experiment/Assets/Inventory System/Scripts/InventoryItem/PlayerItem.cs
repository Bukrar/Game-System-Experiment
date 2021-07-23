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

        InventorySlot[] slots;

        public struct InventorySlot
        {
            public ItemScriptObject item;
            public int number;
        }

        public event Action InventoryUpdated;
        private void Awake()
        {
            slots = new InventorySlot[ItemBagSize];
        }

        public static PlayerItem GetPlayerItem()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<PlayerItem>();
        }

        public ItemScriptObject GetItemInSlot(int slot)
        {
            return slots[slot].item;
        }
        public int GetNumberInSlot(int slot)
        {
            return slots[slot].number;
        }

        public int GetItemBagSize()
        {
            return ItemBagSize;
        }

        public void RemoveFromSlot(int slot, int number)
        {
            slots[slot].number -= number;
            if (slots[slot].number <= 0)
            {
                slots[slot].number = 0;
                slots[slot].item = null;
            }
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
        }

        public bool AddItemToSlot(int slot, ItemScriptObject item, int number)
        {
            if (slots[slot].item != null)
            {
                return AddToFirstEmptySlot(item, number); ;
            }

            var i = FindStack(item);
            if (i >= 0)
            {
                slot = i;
            }

            slots[slot].item = item;
            slots[slot].number += number;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
            return true;
        }

        private int FindSlot(ItemScriptObject item)
        {
            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }
            return i;
        }

        private int FindStack(ItemScriptObject item)
        {
            if (!item.GetcanStackable())
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].item, item))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool AddToFirstEmptySlot(ItemScriptObject item, int number)
        {
            int i = FindSlot();

            if (i < 0)
            {
                return false;
            }

            slots[i].item = item;
            slots[i].number = number;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
            return true;
        }

        private int FindSlot()
        {
            return FindEmptySlot();
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool HasSpaceFor(ItemScriptObject item)
        {
            return FindSlot() >= 0;
        }

        [System.Serializable]
        private struct InventorySlotRecord
        {
            public string itemID;
            public int number;
        }

        public object CaptureState()
        {
            var slotStrings = new InventorySlotRecord[ItemBagSize];
            for (int i = 0; i < ItemBagSize; i++)
            {
                if (slots[i].item != null)
                {
                    slotStrings[i].itemID = slots[i].item.GetItemID();
                    slotStrings[i].number = slots[i].number;
                }
            }
            return slotStrings;
        }

        public void RestoreState(object state)
        {
            var slotStrings = (InventorySlotRecord[])state;
            for (int i = 0; i < ItemBagSize; i++)
            {
                slots[i].item = ItemScriptObject.GetItemDataFromID(slotStrings[i].itemID);
                slots[i].number = slotStrings[i].number;
            }

            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
        }

    }
}