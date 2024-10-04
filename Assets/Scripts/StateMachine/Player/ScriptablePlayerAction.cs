using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptablePlayerAction : ScriptableAction
{
    public bool Momentum { get; set; }


    public PlayerMovement _playerMovement;
    public virtual void OnStartADAction(InputAction.CallbackContext context)
   {
        Momentum = true;
   }

   public virtual void OnEndADAction(InputAction.CallbackContext context)
   {
        Momentum = false;
   }

    public virtual void OnStartWAction(InputAction.CallbackContext context)
    {

    }

    public virtual void OnEndWAction(InputAction.CallbackContext context)
    {

    }

    public virtual void OnStartSAction(InputAction.CallbackContext context)
    {

    }

    public virtual void OnEndSAction(InputAction.CallbackContext context)
    {

    }

    public virtual void OnStartSpaceAction(InputAction.CallbackContext context)
    {

    }

    public virtual void OnEndSpaceAction(InputAction.CallbackContext context)
    {

    }

}
