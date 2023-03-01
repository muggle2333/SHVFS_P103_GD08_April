using UnityEngine;

namespace SHVFS_P103
{
    public class HolderComponent : MonoBehaviour
    {
        public HoldableComponent HeldObject;
        public Transform HolderProxy;

        public void Execute(HoldableComponent holdableComponent = null)
        {
            HeldObject = holdableComponent;
        }
    }
}