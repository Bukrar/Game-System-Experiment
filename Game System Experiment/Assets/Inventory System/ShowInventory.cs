using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ShowInventory : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.I;
        [SerializeField] GameObject itemUI;

        void Start()
        {
            itemUI.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                itemUI.SetActive(!itemUI.activeSelf);
            }
        }
    }
}

