using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableState", menuName = "ScriptableState")]
public class ScriptableState : ScriptableObject
{
    public ScriptableAction Action;
    public List<ScriptableState> ScriptableStateTransitor;
    public string Id;
}
