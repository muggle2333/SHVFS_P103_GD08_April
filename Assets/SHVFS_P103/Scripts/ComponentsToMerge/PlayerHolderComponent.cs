using System.Linq;
using UnityEngine;

namespace SHVFS_P103
{
    public class PlayerHolderComponent : HolderComponent
    {
        [SerializeField]
        private float holdReach;
        [SerializeField]
        private float playerWidth;
        [SerializeField]
        private float playerHeight;
        private float holdDistance => holdReach * Time.deltaTime;
        private PlayerActions playerActions;
        
        private void Awake()
        {
            playerActions = new PlayerActions();
            playerActions.PlayerInput.Enable();
        }

        public void Update()
        {
            if (HeldObject != null) return;
            
            if (playerActions.PlayerInput.InteractPrimary.WasPressedThisFrame())
            {
                TryInteract(transform.forward);
            }
        }
        
        private bool TryInteract(Vector3 direction)
        {
            var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction, holdDistance);
        
            // TODO Invert if to reduce nesting
            if (hits.Length < 1) return false;

            var interactable = hits.FirstOrDefault(h => h.transform.GetComponent<HoldableComponent>() != null).transform;

            if (interactable == null) return false;

            Interact(interactable.GetComponent<InteractableComponentBase>());
            return true;
        }
        
        private void Interact(InteractableComponentBase interactableComponent)
        {
            // interactableComponent.Execute();
        }
    }
}