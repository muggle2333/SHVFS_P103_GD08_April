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
        //playerAnimator.SetFloat("inputDirection",playerMovement);;
        if(playerInputComponent.GetInputDirectionNoramalized().magnitude>0f)
        {
            playerAnimator.Play("Walk");
        }
        else
        {
            playerAnimator.Play("Idle");
        }
        
    }
}
