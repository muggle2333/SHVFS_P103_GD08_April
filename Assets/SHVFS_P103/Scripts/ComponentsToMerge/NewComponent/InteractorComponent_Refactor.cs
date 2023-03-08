using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHVFS_P103
{
    public abstract class InteractorComponent_Refactor : MonoBehaviour
    {
        public int Weight;
        public abstract bool TryInterest();
    }
}

