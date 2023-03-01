using UnityEngine;

public class IngredientSpawnerComponent: InteractableComponentBase
{

    [SerializeField] ObjectComponent spawnedObject;
    private ObjectComponent ownedObject;
    private void Start()
    {
    }
    private ObjectComponent IsSpawned()
    {
        ownedObject= objectPosition.GetComponentInChildren<ObjectComponent>();
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
        IsSpawned();
        if(objectComponent!= null)
        {
            objectComponent.ChangeOwner(objectPosition);
            return;
        }
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

    //Chris Ver
    public IngredientConfiguration Configuration;
    public override void Interaction()
    {
        var ingredient = Instantiate(Configuration.Ingredient);
        ingredient.transform.localScale = Configuration.Scale * Configuration.ScaleFactor;
        Debug.Log($"Instatiated the Ingredient:{ingredient.transform.name}");

    }

}
