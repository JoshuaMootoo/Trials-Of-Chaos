using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private PlayerInputs input = null;
    private Vector2 moveVector = Vector2.zero;

    private bool hasDodged = false;

    private bool hasAttacked = false;

    private Rigidbody rb = null;

    #region XP System
    private int startLevel = 1;
    private int startExp = 0;
    private int startExpToNextLevel = 10;

    public int level { get; private set; }
    public int exp { get; private set; }
    public int expToNextLevel { get; private set; }

    public void LevelStartSetup()
    {
        level = startLevel;
        exp = startExp;
        expToNextLevel = startExpToNextLevel;
    }

    public void GainExp(int amount)
    {
        exp += amount;
        if (exp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;

        expToNextLevel = CalculateExpToNextLevel();
    }

    public int CalculateExpToNextLevel()
    {
        return expToNextLevel * 2;
    }
    #endregion

    [Header("Player Stats - Health")]
    public float currentHealth;
    public float maxHealth;
    public float healthMultiplier;

    [Header("Player Stats - Damage")]
    public float flatDamage;
    public float maxDamage;
    public float damageMultiplier;

    [Header("Player Stats - Speeds")]
    public float movementSpeed = 10;
    public float maxMovementSpeed;
    public float speedMultiplier;

    [SerializeField] private float turnSmoothTime = 0.05f;
    private float currentVelocity;

    #region Awake Function

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        input = new PlayerInputs();
    }
    #endregion
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.useGravity = true;

        rb.velocity = new Vector3(moveVector.x * movementSpeed, rb.velocity.y, moveVector.y * movementSpeed);


        if (moveVector != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveVector.x, moveVector.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        Debug.Log(hasDodged);
        Debug.Log(hasAttacked);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player has took " + damageAmount + " damage");

        if (currentHealth <= 0)
        {
            OnDeath();
        }
        else Debug.Log("Player has " + currentHealth + " health left");
    }

    public void OnDeath() 
    {
        Debug.Log("Player has died");
        Destroy(gameObject);
    }

    //------------------------------------------------------------------------------------
    //                                 Player Input Controls
    //------------------------------------------------------------------------------------

    #region On Enable/Disable
    private void OnEnable()
    {
        input.Enable();
        input.GamePlay.Movement.performed += OnMovementPerformed;
        input.GamePlay.Movement.canceled += OnMovementCancelled;

        input.GamePlay.Dodge.performed += OnDodgePerformed;
        input.GamePlay.Dodge.canceled += OnDodgeCancelled;

        input.GamePlay.Attack.performed += OnAttackPerformed;
        input.GamePlay.Attack.canceled += OnAttackCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.GamePlay.Movement.performed -= OnMovementPerformed;
        input.GamePlay.Movement.canceled -= OnMovementCancelled;

        input.GamePlay.Dodge.performed -= OnDodgePerformed;
        input.GamePlay.Dodge.canceled -= OnDodgeCancelled;

        input.GamePlay.Attack.performed -= OnAttackPerformed;
        input.GamePlay.Attack.canceled -= OnAttackCancelled;
    }
    #endregion
    #region Movement Controls
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }
    #endregion

    public void OnDodgePerformed(InputAction.CallbackContext context)
    {
        hasDodged = context.ReadValueAsButton();
    }
    public void OnDodgeCancelled(InputAction.CallbackContext context)
    {        
        hasDodged = context.ReadValueAsButton();
    }

    public void OnAttackPerformed(InputAction.CallbackContext context)
    {
        hasAttacked = context.ReadValueAsButton();
    }
    public void OnAttackCancelled(InputAction.CallbackContext context)
    {
        hasAttacked = context.ReadValueAsButton();
    }
}
