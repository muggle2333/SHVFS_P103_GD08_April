using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
public  class InteractorComponent : MonoBehaviour
{
    PlayerActions playerActions;
    [SerializeField] float playerHeight;
    [SerializeField] float playerWidth;
    [SerializeField] float interMultiplier;
    private float movementDistance => interMultiplier *Time.deltaTime;
    void Awake()
    {
        playerActions= new PlayerActions();
        playerActions.PlayerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActions.PlayerInput.InteractPrimary.WasPressedThisFrame())
        {
            TryInteract();
        }
    }
    private void TryInteract()
    {
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, transform.forward, movementDistance);
        //var hits = Physics.OverlapCapsule(transform.position, transform.position + Vector3.up * playerHeight, playerWidth);
        if (hits.Length < 1) return;
        //if(hits.Any(hit => hit.transform.GetComponent<InteractableComponent>()!=null))
        //{
        //    Interact();
        //}
        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.name);
            var interactable = hit.transform.GetComponent<InteractableComponent>();
            //Debug.Log(interactable);
            if (interactable == null) return;
            interactable.Interact();
        }
        

    }

}
