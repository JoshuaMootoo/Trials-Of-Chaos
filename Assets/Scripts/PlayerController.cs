using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Gameplay input = null;
    private Vector2 moveVector = Vector2.zero;

    private Rigidbody rb = null;

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

        input = new Gameplay();
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
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }
    #endregion
    #region Movement Controls
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
    #endregion
}
