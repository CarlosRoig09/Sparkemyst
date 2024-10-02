using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptableGroundAction : ScriptablePlayerAction
{

    public override void OnFinishedState()
    {

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
        base.OnStartADAction(context);
    }

    public override void OnEndADAction(InputAction.CallbackContext context)
    {
        base.OnEndADAction(context);
    }

    private IEnumerator ModSpeed(float initialSpeed, float finalspeed, float desiredDistance, float acceleration)
    {
        acceleration = (Mathf.Pow(finalspeed, 2) - Mathf.Pow(initialSpeed, 2)) / (2 * (desiredDistance - Mathf.Abs(PlayerTra.position.x)));
        while (_speed <= _maxSpeed && _speed > 0)
        {
            yield return new WaitForFixedUpdate();
            desiredDistance -= Mathf.Abs(transform.position.x);
            _speed = Mathf.Sqrt(Mathf.Pow(finalspeed, 2) - 2 * acceleration * desiredDistance);
        }
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

    public override void OnSpaceSAction(InputAction.CallbackContext context)
    {

    }

    public override void OnEndSpaceAction(InputAction.CallbackContext context)
    {

    }
}
