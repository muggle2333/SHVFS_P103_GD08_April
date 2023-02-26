using UnityEngine;

public class IngredientDestroyerComponent: InteractableComponent
{
    public override void Interact()
    {
        Debug.Log("Destroy the food");
    }
}
