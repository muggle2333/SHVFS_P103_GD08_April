using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


//Abstract class cannot be used directly，like blueprint, need to inherit from this class
//We inherit  from them, and implement their member
public abstract class InputComponentBase : MonoBehaviour
{
    //signiture is vector2 no arguments
    public abstract Vector2 GetInputDirection();
    public abstract Vector2 GetInputDirectionNoramalized();
}
public class PlayerInputComponent : InputComponentBase
{

    private Vector2 inputDirection;
    private PlayerActions playerActions;

    private void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.PlayerInput.Enable();
    }

/*    void Update()
    {
        inputDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection.y += 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputDirection.y -= 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputDirection.x += 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputDirection.x -= 1;
        }

    }*/
    public override Vector2 GetInputDirection()
    {
        //return inputDirection;
        return playerActions.PlayerInput.Movement.ReadValue<Vector2>();
    }
    public override Vector2 GetInputDirectionNoramalized()
    {
        return GetInputDirection().normalized;  //if(1.1) the move will be too fast
    }
}
