using InventoryItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Drag;

namespace UI.Item
{
    public class ItemSlot : MonoBehaviour, IDragContainer<ItemScriptObject>
    {
        [SerializeField] ItemIcon itemIcon = null;

        int index;
        PlayerItem item;

        public void Setup(PlayerItem item, int index)
        {
            this.item = item;
            this.index = index;
            itemIcon.SetItemIcon(item.GetItemInSlot(index));
        }

        public void AddItem(ItemScriptObject item)
        {
            this.item.AddItemToSlot(index, item);
        }

        public ItemScriptObject GetItem()
        {
            return item.GetItemInSlot(index);
        }

        public void RemoveItem()
        {
            this.item.RemoveFromSlot(index);
        }

    }
}