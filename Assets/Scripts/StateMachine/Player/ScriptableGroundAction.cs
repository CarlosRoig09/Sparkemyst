using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ScriptableGroundAction", menuName = "PlayerActions/GroundAction")]
public class ScriptableGroundAction : ScriptablePlayerAction
{
    public float Direction = 0;
    public override void OnFinishedState()
    {
        _playerMovement.StopCoroutine();
    }

    public override void OnSetState()
    {
        _playerMovement.ModSpeedCoroutine(Momentum);       
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }
    public override void OnStartADAction(InputAction.CallbackContext context)
    {
        Momentum = true;
        _playerMovement.ModSpeedCoroutine(Momentum);
        Direction = context.ReadValue<Vector2>().x;
        _playerMovement.Direction = new Vector2(Direction, _playerMovement.Direction.y);
    }

    public override void OnEndADAction(InputAction.CallbackContext context)
    {
        Momentum = false;
        _playerMovement.ModSpeedCoroutine(Momentum);
        Direction = context.ReadValue<Vector2>().x;
    }

    public override void OnStartWAction(InputAction.CallbackContext context)
    {

    }

    public override void OnEndWAction(InputAction.CallbackContext context)
    {

    }

    public override void OnStartSAction(InputAction.CallbackContext context)
    {

    }

    public override void OnEndSAction(InputAction.CallbackContext context)
    {

    }

    public override void OnStartSpaceAction(InputAction.CallbackContext context)
    {
        _playerMovement.StartJump();
    }

    public override void OnEndSpaceAction(InputAction.CallbackContext context)
    {
        _playerMovement.RelaseJump();
    }
}
