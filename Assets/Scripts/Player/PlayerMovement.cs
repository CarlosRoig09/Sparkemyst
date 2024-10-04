using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class PlayerMovement : StateController
{
    [SerializeField]
    private LayerMask groundLayer;
    public ScriptableState CurrentSatate { get { return currentState; } }
    [Header("SpeedModifiers")]
    [SerializeField]
    private float _maxSpeed;
    public float MaxSpeed { get { return _maxSpeed; } }
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _desiredAccTime;
    [SerializeField]
    private float _desiredDccTime;
    private Vector2 _direction;
    public Vector2 Direction { get { return _direction; } set { _direction = value; } }
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
    private bool _jumping;
    [SerializeField]
    private float _jumpHeight;
    private Rigidbody2D _rb2D;
    public delegate void ModifyMovement(bool start);
    public event ModifyMovement OnModifyMovement;
    // Start is called before the first frame update
   protected override void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _speed = 0;
        _direction = new Vector2(0, 0);
        ((ScriptablePlayerAction)currentState.Action)._playerMovement = this;
        base.Start();
    }

    // Update is called once per frame
  protected override void FixedUpdate()
   {
        base.FixedUpdate();
        _rb2D.velocity = new Vector2(_speed * Time.fixedDeltaTime * _direction.x, _rb2D.velocity.y);
    }

    protected override void Update()
    {
        base.Update();
        if (_jumping)
            ChargeJump();
        if (IsGrounded()&&!Dstates["GROUNDED"].Action.AlreadyCalled)
                ChangeState(Dstates["GROUNDED"]);
        else if(!Dstates["AIR"].Action.AlreadyCalled)
                    ChangeState(Dstates["AIR"]);
    }

    public void ChangeState(ScriptableState newState)
    {
       ((ScriptablePlayerAction)newState.Action).Momentum = ((ScriptablePlayerAction)currentState.Action).Momentum;
        StateTransitor(newState);
        ((ScriptablePlayerAction)currentState.Action)._playerMovement = this;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.65f, groundLayer.value);
    }

    public void ModSpeedCoroutine(bool momentum)
    {
        StopAllCoroutines();
       StartCoroutine(ModSpeed(_speed, momentum ? _maxSpeed : 0, momentum ? _desiredAccTime : _desiredDccTime));
    }

    public void StopCoroutine()
    {
        StopAllCoroutines();
    }

    private IEnumerator ModSpeed(float initialSpeed, float finalspeed, float desiredTime)
    {
        float acceleration = (finalspeed - initialSpeed)/desiredTime;
        float currentTime = 0;
        float distanceCovered = 0;
        do
        {
            yield return new WaitForFixedUpdate();
            currentTime += Time.fixedDeltaTime;
            _speed = initialSpeed + acceleration * currentTime;
            distanceCovered += _speed * Time.fixedDeltaTime;
        } while (currentTime < desiredTime);
        _speed = finalspeed;
        Debug.Log("Distancia total recorrida: " + distanceCovered);
    }

    private void ChargeJump()
    {
        if(_jumpHeight<_maxHeight)
            _jumpHeight += _maxHeight / _timeBwJumps * Time.fixedDeltaTime;
    }

    public void StartJump()
    {
        _jumping = true;
        _jumpHeight = _minHeight;
    }

    public void RelaseJump()
    {
        _jumping = false;
        _rb2D.velocity = new Vector2(_rb2D.velocity.x, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * _jumpHeight));
        _jumpHeight = 0;
    }
}
