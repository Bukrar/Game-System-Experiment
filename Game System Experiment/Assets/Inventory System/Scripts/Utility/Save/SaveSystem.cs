using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility.save
{
    public class SaveSystem : MonoBehaviour
    {
        /// <summary>
        /// ¶s¿…
        /// </summary>
        /// <param name="saveFile"></param>
        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        /// <summary>
        /// ≈™¿…
        /// </summary>
        /// <param name="saveFile"></param>
        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        /// <summary>
        /// ßR¿…
        /// </summary>
        /// <param name="saveFile"></param>
        public void Delete(string saveFile)
        {
            File.Delete(GetPathFromFile(saveFile));
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveEntity saveEntity in FindObjectsOfType<SaveEntity>())
            {
                string id = saveEntity.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveEntity.RestoreState(state[id]);
                }
            }
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromFile(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }

        }

        private void SaveFile(string saveFile, Dictionary<string, object> state)
        {
            string path = GetPathFromFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveEntity saveEntity in FindObjectsOfType<SaveEntity>())
            {
                state[saveEntity.GetUniqueIdentifier()] = saveEntity.CaptureState();
            }

            //state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }



        private string GetPathFromFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}