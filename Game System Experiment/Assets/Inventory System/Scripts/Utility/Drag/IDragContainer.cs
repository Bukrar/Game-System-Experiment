using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Drag
{
    public interface IDragContainer<T> : IDragDestination<T>, IDragSource<T> where T : class
    {
    }
}