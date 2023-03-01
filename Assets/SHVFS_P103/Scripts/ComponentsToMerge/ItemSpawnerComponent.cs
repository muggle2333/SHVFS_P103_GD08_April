using UnityEngine;

namespace SHVFS_P103
{
    public class ItemSpawnerComponent : InteractableComponentBase
    {
        public HoldableComponent ItemPrefab;
        public HolderComponent HolderComponent;
        
        public override void Execute(InteractorComponentBase interactorComponent)
        {
            Debug.Log("trying to spawn...");

            if (HolderComponent.HeldObject != null) return;

            Debug.Log("held object not null...");

            var interactorRequestor = interactorComponent.GetComponent<HolderComponent>();

            if (interactorRequestor != null && interactorRequestor.HeldObject != null) return;

            Debug.Log($"IR NULL: {interactorRequestor != null} | IR HO NULL: {interactorRequestor.HeldObject != null}");

            var spawnedItem = Instantiate(ItemPrefab);
            spawnedItem.SetHolder(HolderComponent);
        }
    }
}