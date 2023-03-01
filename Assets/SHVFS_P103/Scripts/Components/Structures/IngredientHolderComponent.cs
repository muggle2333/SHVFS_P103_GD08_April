using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHolderComponent : InteractableComponentBase
{
    private ObjectComponent ownedObject;
    private bool IsOwnObject()
    {
        ownedObject = objectPosition.GetComponentInChildren<ObjectComponent>();
        return ownedObject;
    }
    public override void Interaction()
    {

    }
    public override void Interact(Transform owner, ObjectComponent objectComponent)
    {
        if (!IsOwnObject()&&objectComponent)
        {
            //Place on the holder
            objectComponent.ChangeOwner(objectPosition);
            Debug.Log("Place on the holder");
            
        }
        else if(IsOwnObject() && objectComponent==null)
        {
            //Give to owner
            ownedObject.ChangeOwner(owner);
            Debug.Log("Take the food");
        }

    }
    
}
