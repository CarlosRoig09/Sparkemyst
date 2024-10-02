using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class PlayerMovement : StateController
{
    [SerializeField]
    private LayerMask groundLayer;

    [Header("SpeedModifiers")]

    [SerializeField]
    private float _minSpeed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _accelerationDistance;
    [SerializeField]
    private float _desaccelerationDistance;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeBwSpeedStart;
    private Vector2 _direction;
    [SerializeField]
    private float _timeBwSpeedEnd;
    [Header("JumpModifiers")]
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _maxHeight;
    [SerializeField]
    private float _minHeight;
    [SerializeField]
    private float _timeBwJumps;
    private Rigidbody2D _rb2D;
    public delegate void ModifyMovement(bool start);
    public event ModifyMovement OnModifyMovement;

    // Start is called before the first frame update
   protected override void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _speed = 0;
        base.Start();
    }

    // Update is called once per frame
  protected override void FixedUpdate()
   {
        base.FixedUpdate();
        if (IsGrounded())
            StateTransitor(Dstates["GROUNDED"]);
        if (currentState is PlayerState currentPlayerState && (_speed != 0 || _direction != Vector2.zero))
        {
            if (currentPlayerState.CanMove)
                OnModifyMovement(true);
            else
                OnModifyMovement(false);
        }

        _rb2D.velocity = new Vector2(_speed * Time.fixedDeltaTime * _direction.x, _rb2D.velocity.y);
    }

    private bool IsGrounded()
   {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.65f, groundLayer.value);
   }

    public IEnumerator ModSpeed(float initialSpeed, float finalspeed, float desiredDistance, float acceleration)
    {
        acceleration = (Mathf.Pow(finalspeed, 2) - Mathf.Pow(initialSpeed, 2)) / (2 * (desiredDistance - Mathf.Abs(transform.position.x)));
        while (_speed <= _maxSpeed && _speed > 0)
        {
            yield return new WaitForFixedUpdate();
            desiredDistance -= Mathf.Abs(transform.position.x);
            _speed = Mathf.Sqrt(Mathf.Pow(finalspeed, 2) - 2 * acceleration * desiredDistance);
        }
    }

    //private IEnumerator ModJumpHeight(float mod)
    //{

    //}
}
