using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SHVFS_P103
{
    public class PlayerInteractorComponent : InteractorComponentBase
    {
        [SerializeField]
        private float interactReach;
        [SerializeField]
        private float playerWidth;
        [SerializeField]
        private float playerHeight;
        private float interactDistance => interactReach * Time.deltaTime;
        private PlayerActions playerActions;
        private event EventHandler evt;
        
        private void Awake()
        {
            playerActions = new PlayerActions();
            playerActions.PlayerInput.Enable();
        }

        public void Update()
        {
            if (playerActions.PlayerInput.InteractPrimary.WasPressedThisFrame())
            {
                TryInteract(transform.forward);
            }
        }
        
        private void TryInteract(Vector3 direction)
        {
            var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction, interactDistance);
        
            if (hits.Length < 1) return;

            var interactableQueue = new List<InteractableComponentBase>();
            
            foreach (var hit in hits)
            {
                var interactable = hit.transform.GetComponent<InteractableComponentBase>();

                if (interactable == null) continue;

                // interactableQueue.Add(interactable.GetComponent<InteractableComponentBase>());
                Interact(interactable.GetComponent<InteractableComponentBase>()); 
            }

            
            // if (interactableQueue.Count < 1) return;
            //
            // Interact(interactableQueue.OrderByDescending(i => i.Weight).ToList()[0]); 
        }
        
        private void Interact(InteractableComponentBase interactableComponent)
        {
            interactableComponent.Execute(this);
        }
    }
}