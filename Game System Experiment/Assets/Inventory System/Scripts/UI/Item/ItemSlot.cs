using InventoryItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Drag;

namespace UI.Item
{
    public class ItemSlot : MonoBehaviour, IItemHolder, IDragContainer<ItemScriptObject>
    {
        [SerializeField] ItemIcon itemIcon = null;

        int index;
        ItemScriptObject item;
        PlayerItem playItem;

        public void Setup(PlayerItem item, int index)
        {
            this.playItem = item;
            this.index = index;
            itemIcon.SetItemIcon(item.GetItemInSlot(index));
        }

        public void AddItems(ItemScriptObject item, int number)
        {
            playItem.AddItemToSlot(index, item, number);
        }

        public ItemScriptObject GetItem()
        {
            return playItem.GetItemInSlot(index);
        }

        public int GetNumber()
        {
            return playItem.GetNumberInSlot(index);
        }

        public int MaxAcceptable(ItemScriptObject item)
        {
            if (playItem.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void RemoveItems(int number)
        {
            playItem.RemoveFromSlot(index, number);
        }



    }
}