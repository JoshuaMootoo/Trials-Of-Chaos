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
    public float movementSpeed = 10;

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


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.useGravity = true;

        rb.velocity = new Vector3(moveVector.x * movementSpeed, rb.velocity.y, moveVector.y * movementSpeed);
    }

    #region Movement Controls
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        //GridManager.Instance.UpdateGrid();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
    #endregion
}
