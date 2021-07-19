using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utility.save
{
    [ExecuteAlways]
    public class SaveEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";

        static Dictionary<string, SaveEntity> globalLookup = new Dictionary<string, SaveEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDic = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDic.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDic[typeString]);
                }
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            if (string.IsNullOrEmpty(property.stringValue) || !Isunique(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }
#endif

        private bool Isunique(string stringValue)
        {
            if (!globalLookup.ContainsKey(stringValue)) return true;

            if (globalLookup[stringValue] = this) return true;

            if (globalLookup[stringValue] == null)
            {
                globalLookup.Remove(stringValue);
                return true;
            }

            if(globalLookup[stringValue].GetUniqueIdentifier()!= stringValue)
            {
                globalLookup.Remove(stringValue);
                return true;
            }

            return false;
        }
    }
}

