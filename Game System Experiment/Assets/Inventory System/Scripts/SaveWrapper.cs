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
