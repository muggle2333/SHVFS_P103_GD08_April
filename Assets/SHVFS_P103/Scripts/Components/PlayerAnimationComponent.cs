using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    private PlayerInputComponent playerInputComponent;
    private Animator playerAnimator;
    void Start()
    {
        playerInputComponent = GetComponent<PlayerInputComponent>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        float playerMovement = Vector2.SqrMagnitude(playerInputComponent.GetInputDirection());
        //playerAnimator.SetFloat("inputDirection",playerMovement);;
        if(playerMovement>0.01f)
        {
            playerAnimator.Play("Walk");
        }
        else
        {
            playerAnimator.Play("Idle");
        }
        
    }
}
