using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementAction", menuName = "MovementAction")]
public class MovementAction : ScriptableAction
{
    public Rigidbody2D RB2D;
    public Vector2 direction;
    public float Speed { get; set; }
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
        RB2D.velocity = Speed * Time.fixedDeltaTime * direction;
    }
}
