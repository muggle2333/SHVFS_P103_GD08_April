using System.Collections.Generic;

public class InteractorComponentCoreBase
{
    public List<InteractorComponentBase_Refactor> Interactions;
    private void Updata()
    {
        foreach (var interaction in Interactions)
        {
            if (interaction.TryInteract()) return;
        }
    }
}