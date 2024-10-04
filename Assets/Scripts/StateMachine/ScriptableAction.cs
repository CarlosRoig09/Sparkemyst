using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableAction : ScriptableObject
{
    public bool AlreadyCalled;
    public virtual void OnFinishedState()
    {
        AlreadyCalled = false;
    }

    public virtual void OnSetState()
    {
        AlreadyCalled = true;
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

}
