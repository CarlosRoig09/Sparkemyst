using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Inputs
{
    Movement,
    Jump
}
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private PlayerInput _playerInput;
    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _onMovement;
    private InputAction _onJump;
    private PlayerMovement _playerMovement;
    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        try
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.actions = _actionAsset;
            _onMovement = _playerInput.actions["Move"];
            _onJump = _playerInput.actions["Jump"];
        }
        catch { Debug.LogError("ERROR: PlayerInput component is missing"); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        OnStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStartGame()
    {
        SubscribeEvents(new Inputs[] { Inputs.Movement, Inputs.Jump});
        _playerMovement.OnModifyMovement += ModifyMovement;
    }

    private void ModifyMovement(bool start)
    {
        if (start)
            SubscribeEvents(new Inputs[] { Inputs.Movement });
        else
            DesubscribeEvents(new Inputs[] { Inputs.Movement });
    }

    public void SubscribeEvents(Inputs[] inputs)
    {
        foreach (Inputs input in inputs)
        {
            switch (input)
            {
                case Inputs.Movement:
                    _onMovement.started += OnStartMovement;
                    _onMovement.canceled += OnEndMovement;
                    break;
                case Inputs.Jump:
                    _onJump.started += OnStartJump;
                    _onJump.canceled += OnEndJump;
                    break;
                default:
                    Debug.LogError("Error: This input doesn't exist");
                    break;
            }
        }
    }

    public void DesubscribeEvents(Inputs[] inputs)
    {
        foreach (Inputs input in inputs)
        {
            switch (input)
            {
                case Inputs.Movement:
                    _onMovement.started -= OnStartMovement;
                    //_onMovement.canceled -= OnEndMovement;
                    break;
                case Inputs.Jump:
                    _onJump.started -= OnStartJump;
                    _onJump.canceled -= OnEndJump;
                    break;
                default:
                    Debug.LogError("Error: This input doesn't exist");
                    break;
            }
        }
    }

    void OnStartMovement(InputAction.CallbackContext context)
    {
    }

    void OnEndMovement(InputAction.CallbackContext context)
    {
    }

    void OnStartJump(InputAction.CallbackContext context)
    {
    }

    void OnEndJump(InputAction.CallbackContext context)
    {
    }
}
