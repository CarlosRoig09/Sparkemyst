using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    GROUNDED,
    JUMP,
    DASH,
    WALLJUMP,
    WALLSLIDING,
    GLIDE,
    MOVING,
    STILL
}
public class PlayerMovement : StateController
{
    [SerializeField]
    private float _minSpeed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedMod;
    [SerializeField]
    private float _timeBwSpeeds;
    [SerializeField]
    private float JumpForce;
    private PlayerState _playerState;
    private Rigidbody2D _rb2D;

    // Start is called before the first frame update
   protected override void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _speed = 0;
        base.Start();
    }

    // Update is called once per frame
  protected override void Update()
    {
        base.Update();
        if (_rb2D.velocity == Vector2.zero &&_speed == 0)
        {
            Debug.Log("Still");
            _playerState = PlayerState.STILL;
            StateTransitor(Dstates["STILL"]);
        }
    }

    public void StartMovement(Vector2 direction)
    {
        StopAllCoroutines();
        var movementAction = (MovementAction)Dstates["MOVEMENT"].Action;
        movementAction.direction = direction;
        movementAction.RB2D = _rb2D;
        StateTransitor(Dstates["MOVEMENT"]);
        _speed = _minSpeed;
        StartCoroutine(ModSpeed(_speedMod, _speed, _maxSpeed));
        Debug.Log("Go");
    }

    public void PlayerEndMovement()
    {
        Debug.Log("Stop");
        StopAllCoroutines();
        StartCoroutine(ModSpeed(_speedMod * -1, _speed * -1, 0));
    }

    private IEnumerator ModSpeed(float mod, float initialSpeed, float finalSpeed)
    {
        var movementAction = (MovementAction)Dstates["MOVEMENT"].Action;
        while (_speed < _maxSpeed && _speed > 0)
        {
            yield return new WaitForFixedUpdate();
            _speed += mod * Time.fixedDeltaTime + initialSpeed;
            movementAction.Speed = _speed;
        }
        _speed = finalSpeed;
        movementAction.Speed = _speed;
    }
}
