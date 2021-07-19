using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItem
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class ItemScriptObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string itemID = null;
        [SerializeField] string dispayName = null;
        [SerializeField] [TextArea] string description = null;
        [SerializeField] Sprite icon = null;
        [SerializeField] bool canStackable = false;

        static Dictionary<string, ItemScriptObject> itemLookupCache;

        public static ItemScriptObject GetItemDataFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, ItemScriptObject>();
                var itemList = Resources.LoadAll<ItemScriptObject>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.Log("­«´_" + item.itemID);
                        continue;
                    }
                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public string GetDispayName()
        {
            return dispayName;
        }

        public bool GetcanStackable()
        {
            return canStackable;
        }

        public string GetDescription()
        {
            return description;
        }

        public void OnAfterDeserialize()
        {

        }

        public void OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = Guid.NewGuid().ToString();
            }
        }
    }
}

