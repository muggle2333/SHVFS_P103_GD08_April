using UnityEngine;

public class IngredientDestroyerComponent: InteractableComponentBase
{
   
    public override void Interact(Transform owner,ObjectComponent objectComponent)
    {
        objectComponent.DestroyObject();
        Debug.Log("Destroy the food");

    }
    public override void Interaction()
    {
        
    }
}
