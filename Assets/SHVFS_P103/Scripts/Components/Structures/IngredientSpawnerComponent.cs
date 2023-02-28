using UnityEngine;

public class IngredientSpawnerComponent: InteractableComponent
{

    [SerializeField] ObjectComponent spawnedObject;
    private ObjectComponent ownedObject;
    private void Start()
    {
    }
    private bool IsSpawned()
    {
        ownedObject = objectPosition.GetComponentInChildren<ObjectComponent>();
        return ownedObject;
    }
    public override void Interact(Transform owner, ObjectComponent objectComponent)
    {
        //if no object on the top,Interact = >create one on the top
        //if there is one on the top, Interact
        //1. if has one = > put it down
        //2. if don't = > give one to the owner
        if (!IsSpawned())
        {
            //Spawn one
            GameObject.Instantiate(spawnedObject, objectPosition.position, transform.rotation, objectPosition);
            Debug.Log("Spawn the food");
        }
        else if(objectComponent == null)
        {
            //Take it
            ownedObject.ChangeOwner(owner);
            Debug.Log("Take the food");
        }
        else
        {
            //Put it down
            objectComponent.PutDownObject(owner);
            Debug.Log("Put down the food");
        }
        
    }

}
