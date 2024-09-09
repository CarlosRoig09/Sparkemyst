using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Inputs
{
    Movement
}
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private PlayerInput _playerInput;
    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _onMovement;
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
        }
        catch { Debug.LogError("ERROR: PlayerInput component is missing"); }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnStartGame();
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStartGame()
    {
        SubscribeEvents(new Inputs[] { Inputs.Movement });
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

                    break;
                default:
                    Debug.LogError("Error: This input doesn't exist");
                    break;
            }
        }
    }

    void OnStartMovement(InputAction.CallbackContext context)
    {
        _playerMovement.StartMovement(context.ReadValue<Vector2>());
    }

    void OnEndMovement(InputAction.CallbackContext context)
    {
        _playerMovement.PlayerEndMovement();
    }
}
