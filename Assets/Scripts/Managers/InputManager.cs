using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    //  PlayerInputs Asset
    [SerializeField] private InputActionAsset playerInputs;

    //  Action Map Name
    [SerializeField] private string gamePlay = "Gameplay";
    [SerializeField] private string menu = "Menu";

    //  Gameplay Actions
    [SerializeField] private string move = "Movement";
    [SerializeField] private string dodge = "Dodge";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string pause = "Pause";
    
    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction attackAction;
    private InputAction pauseAction;

    //  Menu Actions
    [SerializeField] private string navigation = "Navigation";
    [SerializeField] private string confirm = "Confirm";
    [SerializeField] private string back = "Back";    

    private InputAction navigationAction;
    private InputAction confirmAction;
    private InputAction backAction;

    public Vector2 MoveInput { get; private set; }
    public bool DodgeInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool PauseInput { get; private set; }
    public Vector2 NavigationInput { get; private set; }
    public bool ConfirmInput { get; private set; }
    public bool BackInput { get; private set; }

    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);

        moveAction = playerInputs.FindActionMap(gamePlay).FindAction(move);
        dodgeAction = playerInputs.FindActionMap(gamePlay).FindAction(dodge);
        attackAction = playerInputs.FindActionMap(gamePlay).FindAction(attack);
        pauseAction = playerInputs.FindActionMap(gamePlay).FindAction(pause);

        navigationAction = playerInputs.FindActionMap(menu).FindAction(navigation);
        confirmAction = playerInputs.FindActionMap(menu).FindAction(confirm);
        backAction = playerInputs.FindActionMap(menu).FindAction(back);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        dodgeAction.performed += context => DodgeInput = true;
        dodgeAction.canceled += context => DodgeInput = false;

        attackAction.performed += context => AttackInput = true;
        attackAction.canceled += context => AttackInput = false;

        pauseAction.performed += context => PauseInput = true;
        pauseAction.canceled += context => PauseInput = false;

        navigationAction.performed += context => NavigationInput = context.ReadValue<Vector2>();
        navigationAction.canceled += context => NavigationInput = Vector2.zero;
        
        confirmAction.performed += context => ConfirmInput = true;
        confirmAction.canceled += context => ConfirmInput = false;
        
        backAction.performed += context => BackInput = true;
        backAction.canceled += context => BackInput = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        dodgeAction.Enable();
        attackAction.Enable();
        pauseAction.Enable();

        navigationAction.Enable();
        confirmAction.Enable();
        backAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        dodgeAction.Disable();
        attackAction.Disable();
        pauseAction.Disable();

        navigationAction.Disable();
        confirmAction.Disable();
        backAction.Disable();
    }
}
