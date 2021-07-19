using InventoryItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Item
{
    [RequireComponent(typeof(Image))]
    public class ItemIcon : MonoBehaviour
    {
        public void SetItemIcon(ItemScriptObject item)
        {
            Image iconImage = GetComponent<Image>();
            if (item != null)
            {
                iconImage.enabled = true;
                iconImage.sprite = item.GetIcon();
            }
            else
            {
                iconImage.enabled = false;
                iconImage.sprite = null;
            }
        }
    }

}
