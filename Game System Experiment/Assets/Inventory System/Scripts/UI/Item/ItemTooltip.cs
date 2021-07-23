using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InventoryItem
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;

        public void Setup(ItemScriptObject item)
        {
            titleText.text = item.GetDispayName();
            bodyText.text = item.GetDescription();
        }
    }
}
