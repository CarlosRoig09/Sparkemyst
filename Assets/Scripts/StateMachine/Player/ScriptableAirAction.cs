using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ScriptableAirAction", menuName = "PlayerActions/AirAction")]
public class ScriptableAirAction : ScriptablePlayerAction
{
    public override void OnFinishedState()
    {
        _playerMovement.StopCoroutine();
    }

    public override void OnSetState()
    {
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }
    public override void OnStartADAction(InputAction.CallbackContext context)
    {
        //Momentum = true;
    }

    public override void OnEndADAction(InputAction.CallbackContext context)
    {
        Momentum = false;
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
    }

    public override void OnEndSpaceAction(InputAction.CallbackContext context)
    {
    }
}
