using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class InteractableComponent : MonoBehaviour
{
    [SerializeField] protected Transform objectPosition;
    public abstract void Interact(Transform owner, ObjectComponent objectComponent);
}
