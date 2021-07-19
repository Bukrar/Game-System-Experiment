using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.save
{
    public interface ISaveable
    {
        object CaptureState();

        void RestoreState(object state);
    }
}
