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

        //if (!IsSpawned())
        //{
        //    //Spawn one
        //    GameObject.Instantiate(spawnedObject, objectPosition.position, transform.rotation, objectPosition);
        //    Debug.Log("Spawn the food");
        //}
        //else if(objectComponent == null)
        //{
        //    //Take it
        //    ownedObject.ChangeOwner(owner);
        //    Debug.Log("Take the food");
        //}
        //else
        //{
        //    //Put it down
        //    objectComponent.PutDownObject(owner);
        //    Debug.Log("Put down the food");
        //}
        if (objectComponent != null) return;
        if(!IsSpawned())
        {
            //Spawn one
            Instantiate(spawnedObject, objectPosition.position, transform.rotation, objectPosition);
            Debug.Log("Spawn the food");
        }
        else
        {
            //Take it
            ownedObject.ChangeOwner(owner);
            Debug.Log("Take the food");
        }

    }

}
