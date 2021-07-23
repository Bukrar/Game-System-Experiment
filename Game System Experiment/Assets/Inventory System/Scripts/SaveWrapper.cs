using InventoryItem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.save;

public class SaveWrapper : MonoBehaviour
{
    KeyCode saveKey = KeyCode.S;
    KeyCode loadKey = KeyCode.L;
    KeyCode deleteKey = KeyCode.D;
    const string defaultSaveFile = "save";

    private void Update()
    {
        if (Input.GetKeyDown(saveKey))
        {
            Save();
        }
        if (Input.GetKeyDown(loadKey))
        {
            Load();
        }
        if (Input.GetKeyDown(deleteKey))
        {
            Delete();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    ClickPickup pickup = hit.collider.gameObject.GetComponent<ClickPickup>();
                    if (pickup != null)
                    {
                        pickup.ClickItem();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {   
            var a = GameObject.FindGameObjectWithTag("Player");
            a.GetComponent<ItemDropper>().DropItem(ItemScriptObject.GetItemDataFromID("4361f5bd-7e42-425d-9bfe-e325c7ab1a05"));
        }
    }

    private void Delete()
    {
        GetComponent<SaveSystem>().Delete(defaultSaveFile);
    }

    private void Load()
    {
        GetComponent<SaveSystem>().Load(defaultSaveFile);
    }

    private void Save()
    {
        GetComponent<SaveSystem>().Save(defaultSaveFile);
    }
}
