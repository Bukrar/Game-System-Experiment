using InventoryItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Item
{
    public class ItemContent : MonoBehaviour
    {
        [SerializeField] ItemSlot itemPrefab = null;
        PlayerItem playItem;

        private void Awake()
        {
            playItem = PlayerItem.GetPlayerItem();
            playItem.InventoryUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < playItem.GetItemBagSize(); i++)
            {
                var itemUI = Instantiate(itemPrefab, transform);
                itemUI.Setup(playItem, i);
            }
        }
    }
}
