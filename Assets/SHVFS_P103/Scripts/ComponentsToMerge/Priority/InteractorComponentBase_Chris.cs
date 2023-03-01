using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractorComponentBase_Refactor : MonoBehaviour
{
    public int weight;
    public abstract bool TryInteract();
}
