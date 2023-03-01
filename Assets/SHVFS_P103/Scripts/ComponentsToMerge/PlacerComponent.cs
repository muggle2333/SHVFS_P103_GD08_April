using UnityEngine;

namespace SHVFS_P103
{
    public class PlacerComponent : MonoBehaviour
    {
        public HolderComponent HolderComponent;
        [SerializeField]
        private float placeReach;
        [SerializeField]
        private float playerWidth;
        [SerializeField]
        private float playerHeight;
        private float placeDistance => placeReach * Time.deltaTime;
        
        public void Execute(HoldableComponent holdableComponent)
        {
            TryPlace(holdableComponent, transform.forward);
        }
        
        private void TryPlace(HoldableComponent holdableComponent, Vector3 direction)
        {
            var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction, placeDistance);

            if (hits.Length < 1)
            {
                Place(holdableComponent, null); 
            }
            
            foreach (var hit in hits)
            {
                var holder = hit.transform.GetComponent<HolderComponent>();

                if (holder == null) continue;

                if (holder == HolderComponent) continue;
                
                Place(holdableComponent, holder);
                return;
            }
            
            Place(holdableComponent, null); 
        }
        
        private void Place(HoldableComponent holdableComponent, HolderComponent holderComponent)
        {
            holdableComponent.SetHolder(holderComponent);
        }
    }
}