using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public  class InteractorComponent : MonoBehaviour
{
    PlayerActions playerActions;
    [SerializeField] float playerHeight;
    [SerializeField] float playerWidth;
    [SerializeField] float interMultiplier;
    [SerializeField] Transform objectPosition;
    private float movementDistance => interMultiplier *Time.deltaTime;
    private float rayDistance = 0;
    private ObjectComponent objectComponent;

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
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, transform.forward, movementDistance);
        foreach (var hit in hits)
        {
            rayDistance = hit.distance;
        }
    }
    //if structer has object =>get
    //if player has one on hand =>put down
    private void TryInteract()
    {
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, transform.forward, movementDistance);
        if (hits.Length < 1) return;
        //if(hits.Any(hit => hit.transform.GetComponent<InteractableComponent>()!=null))
        //{
        //    Interact();
        //}

        foreach (var hit in hits)
        {
            rayDistance = hit.distance;
            //Debug.Log(hit.collider.name);
            var interactable = hit.transform.GetComponent<InteractableComponent>();
            if (interactable == null) continue;
            objectComponent = objectPosition.GetComponentInChildren<ObjectComponent>();
            interactable.Interact(objectPosition,objectComponent);
        }
        

    }
    void OnDrawGizmos()
    {
        float radius = playerWidth;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
        Gizmos.DrawWireSphere(transform.position+ transform.forward * rayDistance, radius);
        Gizmos.DrawWireSphere(transform.position+ Vector3.up * playerHeight+ transform.forward * rayDistance, radius);
    }

}
