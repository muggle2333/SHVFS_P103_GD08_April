using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientComponent : InteractableComponentBase
{
    public override void Interact(Transform owner, ObjectComponent objectComponent)
    {
        Debug.Log($"Interact");
    }
    public override void Interaction()
    {
        Debug.Log($"Interact");
    }
}
