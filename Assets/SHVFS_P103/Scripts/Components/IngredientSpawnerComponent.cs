using UnityEngine;

public class IngredientSpawnerComponent: InteractableComponent
{
    public override void Interact()
    {
        Debug.Log("Spawn the food");
    }
}
