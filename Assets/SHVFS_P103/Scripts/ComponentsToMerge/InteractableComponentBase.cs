using UnityEngine;

namespace SHVFS_P103
{
    public abstract class InteractableComponentBase : MonoBehaviour
    {
        public int Weight;
        public abstract void Execute(InteractorComponentBase interactorComponent);
    }
}