using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public ScriptableState currentState;
    protected Dictionary<string, ScriptableState> Dstates;
    public List<ScriptableState> Lstates;

    protected virtual void Start()
    {
        Dstates = new Dictionary<string, ScriptableState>();
        foreach (var state in Lstates)
        {
            Dstates.Add(state.Id, state);
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        currentState.Action.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        currentState.Action.OnFixedUpdate();
    }

    public void StateTransitor(ScriptableState state)
    {
        if (currentState.ScriptableStateTransitor.Contains(state))
        {
            currentState.Action.OnFinishedState();
            currentState = state;
            currentState.Action.OnSetState();
        }
    }
}
