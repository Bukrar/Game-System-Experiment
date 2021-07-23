using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Drag
{
    public interface IDragSource<T> where T : class
    {
        T GetItem();
        int GetNumber();
        void RemoveItems(int number);
    }
}