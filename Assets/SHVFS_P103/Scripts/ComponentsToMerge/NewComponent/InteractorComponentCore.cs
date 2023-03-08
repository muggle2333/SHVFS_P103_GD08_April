using SHVFS_P103;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorComponentCore : MonoBehaviour
{
    public List<InteractorComponent_Refactor> Interactions;
    // public void OnEnable()
    // {
    //     Interactions = Interactions.OrderByDescending(i => i.Weight).ToList();
    // }
    private void Update()
    {
        foreach(var interaction in Interactions)
        {
            if (interaction.TryInterest())
                return;
        }
    }
}
