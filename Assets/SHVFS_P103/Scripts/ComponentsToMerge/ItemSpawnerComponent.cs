namespace SHVFS_P103
{
    public class ItemSpawnerComponent : InteractableComponentBase
    {
        public HoldableComponent ItemPrefab;
        public HolderComponent HolderComponent;
        
        public override void Execute(InteractorComponentBase interactorComponent)
        {
            if (HolderComponent.HeldObject != null) return;

            var interactorRequestor = interactorComponent.GetComponent<HolderComponent>();

            if (interactorRequestor != null && interactorRequestor.HeldObject != null) return;
            
            var spawnedItem = Instantiate(ItemPrefab);
            spawnedItem.SetHolder(HolderComponent);
        }
    }
}