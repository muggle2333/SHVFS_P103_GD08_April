using UnityEngine;

public class IngredientDestroyerComponent: InteractableComponent
{
   
    public override void Interact(Transform owner,ObjectComponent objectComponent)
    {
        objectComponent.DestroyObject();
        Debug.Log("Destroy the food");

    }
}
