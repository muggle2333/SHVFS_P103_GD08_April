using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField]
    private PlayerInputComponent playerInputComponent;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float raycastLength;
    private float rayDistance;
    private Vector3 movementDirection;
    private Vector3 targetLookRotation;

    private float journeyTime = 1.0f;
    private float startTime;

    //Chris for capsulecast
    [SerializeField] private float playerWidth;
    [SerializeField]private float playerHeight;
    private float movementDistance => movementSpeed * Time.deltaTime;
    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        //Vector2 inputDirection = GetComponent<PlayerInputComponent>().GetInputDirection();
        Vector2 inputDirection = playerInputComponent.GetInputDirection();
        float fracComplete = (Time.time - startTime) / journeyTime;
        if (inputDirection.magnitude>0)
        {
            Vector3 targetPosition;
            Vector3 targetLookRotation;
            //float fracComplete = (Time.time - startTime) / journeyTime;

            movementDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;

            //Shperecast the blocking structure
            Vector3 hitNormal = GetBlockedDirection(movementDirection);
            float hitRelation = Vector3.Dot(hitNormal, movementDirection);
            if(hitRelation>=0) //not block, angle <=90
            {
                targetPosition = transform.position + movementSpeed * movementDirection * Time.deltaTime;
                targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, fracComplete);
                transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
            }
            else //block, angle >90
            {
                //if the direction is not totally opposite => move to the side
                Vector3 hitTangent = new Vector3(hitNormal.z, 0, -1f*hitNormal.x);
                float hitTangentRelation = Vector3.Dot(hitTangent, movementDirection);
                if(hitTangentRelation !=0)
                {
                    if (hitTangentRelation < 0)
                    {
                        hitTangent = -hitTangent; // Set the tangent to the side total diretion with the movementdirection
                        hitTangentRelation = -hitTangentRelation;
                        
                    }
                    targetPosition = transform.position + movementSpeed * hitTangent * Time.deltaTime;
                    targetLookRotation = Vector3.Slerp(transform.forward, hitTangent * hitTangentRelation, fracComplete);
                    transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
                }
                
            }
        }
        //Chris method1
/*        if (!(playerInputComponent.GetInputDirectionNoramalized().magnitude > 0f)) return;
        movementDirection = new Vector3(playerInputComponent.GetInputDirectionNoramalized().x, 0f, playerInputComponent.GetInputDirectionNoramalized().y);
        targetLookRotation = Vector3.Slerp(transform.position, movementDirection, fracComplete);
        transform.rotation=Quaternion.LookRotation(targetLookRotation, Vector3.up);

        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirection, movementDistance);
        if(canMove)
        {
            var targetPosition = transform.position + movementSpeed * movementDirection* Time.deltaTime;
            transform.position = targetPosition;
            return;
        }
        var movementDirectionX = new Vector3(movementDirection.x, 0f, 0f).normalized;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirectionX, movementDistance);
        if(canMove)
        {
            var targetPosition = transform.position + movementSpeed * movementDirectionX* Time.deltaTime;
            transform.position = targetPosition;
            return;
        }
        var movementDirectionZ = new Vector3(0f, 0f, movementDirection.z).normalized;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirectionZ, movementDistance);
        if (canMove)
        {
            var targetPosition = transform.position + movementSpeed * movementDirectionZ * Time.deltaTime;
            transform.position = targetPosition;

        }*/


        

        

        
    }
   
    //Raycast the collider
    private Vector3 GetBlockedDirection(Vector3 movementDirction)
    {
        //!!!Physics.SphereCast => cast shpere
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, raycastLength, movementDirction, out hitInfo, raycastLength))
        {
            if(hitInfo.collider.GetComponent<StructureComponent>() != null)
            {
                rayDistance = hitInfo.distance;
                return hitInfo.normal;
            }
        }
        //overlapShpere return collider[] not hit info
        /*Collider[] hitColliders = Physics.OverlapSphere(transform.position, raycastLength);
        if(hitColliders.Length>0)
        {
            foreach(var hitCollider in hitColliders)
            {
                Debug.Log(hitCollider.name);
            }
        }*/
        return new Vector3(0,0,0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, movementDirection * rayDistance);
        Gizmos.DrawWireSphere(transform.position + movementDirection * rayDistance, raycastLength);
    }

}
