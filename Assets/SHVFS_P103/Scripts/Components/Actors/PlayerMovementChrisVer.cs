using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovementChrisVer : MonoBehaviour
{
    [SerializeField]
    private PlayerInputComponent playerInputComponent;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotateSpeed;

    private float journeyTime = 1.0f;
    private float startTime;

    //Chris for capsulecast
    [SerializeField] private float playerWidth;
    [SerializeField] private float playerHeight;

    private SphereCollider collider;
    private float movementDistance => movementSpeed * Time.deltaTime;
    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        //Chris method1
        Vector2 inputDirection = playerInputComponent.GetInputDirection();
        float fracComplete = (Time.time - startTime) / journeyTime;
        if (!(playerInputComponent.GetInputDirectionNoramalized().magnitude > 0f)) return;
        var movementDirection = new Vector3(playerInputComponent.GetInputDirectionNoramalized().x, 0f, playerInputComponent.GetInputDirectionNoramalized().y);
        var targetLookRotation = Vector3.Slerp(transform.position, movementDirection, fracComplete);
        transform.rotation = Quaternion.LookRotation(targetLookRotation, Vector3.up);

        //var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirection, movementDistance);
        //if (canMove)
        //{
        //    var targetPosition = transform.position + movementSpeed * movementDirection * Time.deltaTime;
        //    transform.position = targetPosition;
        //    return;
        //}
        //var movementDirectionX = new Vector3(movementDirection.x, 0f, 0f).normalized;
        //canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirectionX, movementDistance);
        //if (canMove)
        //{
        //    var targetPosition = transform.position + movementSpeed * movementDirectionX * Time.deltaTime;
        //    transform.position = targetPosition;
        //    return;
        //}
        //var movementDirectionZ = new Vector3(0f, 0f, movementDirection.z).normalized;
        //canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirectionZ, movementDistance);
        //if (canMove)
        //{
        //    var targetPosition = transform.position + movementSpeed * movementDirectionZ * Time.deltaTime;
        //    transform.position = targetPosition;

        //}


        if (TryMove(movementDirection)) return;
        if (TryMove(new Vector3(movementDirection.x, 0f, 0f).normalized))return;
        TryMove(new Vector3(0f, 0f, movementDirection.z).normalized);
    }

    //
    private bool TryMove(Vector3 direction)
    {
        /*        RaycastHit hit;
                var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction,out hit, movementDistance);
                if (canMove || hit.collider.GetComponent<StructureComponent>()==null)
                {
                    var targetPosition = transform.position + movementSpeed * direction * Time.deltaTime;
                    transform.position = targetPosition;
                    return true;
                }
                return false;*/
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction, movementDistance);
        //limit nesting
        if(hits.Length>=1)
        {
            if(!hits.All(hit => hit.transform.GetComponent<StructureComponent>()==null)) return false;
            Move(direction);
            return true;
        }
        Move(direction);
        return false;
    }
    private void Move(Vector3 direction)
    {
        var targetPosition = transform.position + movementSpeed * direction * Time.deltaTime;
        transform.position = targetPosition;
    }


}
