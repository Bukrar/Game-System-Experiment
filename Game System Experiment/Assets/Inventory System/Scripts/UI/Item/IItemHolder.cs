using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItem
{
    public interface IItemHolder
    {
        ItemScriptObject GetItem();
    }
}
