using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItem;
using Utility.Drag;

namespace UI.Item
{
    public class ItemDropTarget : MonoBehaviour, IDragDestination<ItemScriptObject>
    {
        public void AddItems(ItemScriptObject item, int number)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ItemDropper>().DropItem(item);
        }

        public int MaxAcceptable(ItemScriptObject item)
        {
            return int.MaxValue;
        }
    }
}
