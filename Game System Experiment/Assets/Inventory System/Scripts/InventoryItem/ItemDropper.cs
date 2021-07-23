using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.save;

namespace InventoryItem
{
    public class ItemDropper : MonoBehaviour, ISaveable
    {
        private List<Pickup> droppedItems = new List<Pickup>();

        public void DropItem(ItemScriptObject item)
        {
            SpawnPickup(item, GetDropLocation(), 1);
        }

        protected virtual Vector3 GetDropLocation()
        {
            return transform.position;
        }

        public void SpawnPickup(ItemScriptObject item, Vector3 spawnLocation, int number)
        {
            var pickup = item.SpawnPickup(spawnLocation, number);
            droppedItems.Add(pickup);
        }

        [Serializable]
        private struct DropRecord
        {
            public string itemID;
            public SerializableVector3 position;
            public int number;
        }


        public object CaptureState()
        {
            RemoveDestroyedDrops();
            var droppedItemList = new DropRecord[droppedItems.Count];
            for (int i = 0; i < droppedItemList.Length; i++)
            {
                droppedItemList[i].itemID = droppedItems[i].GetItem().GetItemID();
                droppedItemList[i].position = new SerializableVector3(droppedItems[i].transform.position);
                droppedItemList[i].number = droppedItems[i].GetNumber();
            }
            return droppedItemList;
        }

        public void RestoreState(object state)
        {
            var droppedItemsList = (DropRecord[])state;
            foreach (var item in droppedItemsList)
            {
                var pickupItem = ItemScriptObject.GetItemDataFromID(item.itemID);
                Vector3 position = item.position.ToVector();
                int number = item.number;
                SpawnPickup(pickupItem, position, number);
            }
        }

        private void RemoveDestroyedDrops()
        {
            var newList = new List<Pickup>();
            foreach (var item in droppedItems)
            {
                if (item != null)
                {
                    newList.Add(item);
                }
            }
            droppedItems = newList;
        }
    }
}