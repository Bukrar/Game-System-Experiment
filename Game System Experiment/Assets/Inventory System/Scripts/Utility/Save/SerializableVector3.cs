using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.save
{
    [Serializable]
    public class SerializableVector3 : MonoBehaviour
    {
        float x, y, z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
