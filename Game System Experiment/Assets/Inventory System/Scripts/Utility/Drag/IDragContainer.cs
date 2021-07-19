using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Drag
{
    public interface IDragContainer<T> where T : class
    {
        T GetItem();

        void RemoveItem();

        void AddItem(T item);

        //int GetNumber();

        //int MaxAcceptable(T item);
    }
}