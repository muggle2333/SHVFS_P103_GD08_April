using UnityEngine;

namespace SHVFS_P103
{
    public class HoldableComponent : InteractableComponentBase
    {
        public Rigidbody Rigidbody;
        public HolderComponent HolderComponent;
        
        public override void Execute(InteractorComponentBase interactorComponent)
        {
            var holdRequestorComponent = interactorComponent.GetComponent<HolderComponent>();
            
            if (holdRequestorComponent == null) return;
            
            SetHolder(holdRequestorComponent);
        }
        
        public void SetHolder(HolderComponent holdRequestorComponent)
        {
            // Drop
            if (holdRequestorComponent == null)
            {
                if (HolderComponent != null)
                {
                    HolderComponent.Execute();
                }
                
                HolderComponent = null;
                transform.SetParent(HoldableParentComponent.Instance.transform);
                Rigidbody.isKinematic = false;
                return;
            }
            
            // Pickup
            if (holdRequestorComponent.HeldObject == null)
            {
                Debug.Log($"NAME 1: {holdRequestorComponent.transform.name}");

                if (HolderComponent != null)
                {
                    HolderComponent.Execute();
                }
                
                HolderComponent = holdRequestorComponent;
                HolderComponent.Execute(this);
                transform.SetParent(HolderComponent.HolderProxy);
                Rigidbody.isKinematic = true;
                
                //transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                return;
            }
            
            // Drop, place
            if (holdRequestorComponent.HeldObject != null && holdRequestorComponent.HeldObject == this)
            {
                Debug.Log($"NAME 2: {holdRequestorComponent.transform.name}");

                var placerComponent = holdRequestorComponent.GetComponent<PlacerComponent>();

                if (placerComponent == null) return;
                
                if (HolderComponent != null)
                {
                    HolderComponent.Execute();
                }

                placerComponent.Execute(this);
            }
        }
    }
}