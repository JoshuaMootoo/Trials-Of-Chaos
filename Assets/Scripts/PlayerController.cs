using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    private Rigidbody rb = null;

    private enum PlayerState
    {
        Normal,
        Attacking,
        Dodging,
    }

    private PlayerState state;

    [SerializeField] private InputManager inputManager;
    
    //  Start Stats
    public float startHealth;
    public float startDamage;
    public float startSpeed;
    public float startDodgeSpeed;

    //  Health Vaeiables
    public float health;
    public float maxHealth;

    //  Movement Variables
    public float movementSpeed;
    private Vector2 moveVector = Vector2.zero;
    [SerializeField] private float turnSmoothTime = 0.05f;
    private float currentVelocity;

    //  Dodge Variables
    private bool DodgedButtonPressed = false;
    private Vector2 dodgeVector = Vector2.zero;
    public float dodgeSpeed;

    public float levelMultiplier = 1;

    public void PlayerStartStats()
    {
        health = startHealth;
        maxHealth = startHealth;
        damage = startDamage;
        dodgeSpeed = startDodgeSpeed;

        weaponsNum = Random.Range(1, 4);
    }

    //------------------------------------------------------------------------------------
    //                                 Player EXP System
    //------------------------------------------------------------------------------------
    #region EXP System
    private int startLevel = 1;
    private int startExp = 0;
    private int startExpToNextLevel = 10;

    public int level { get; private set; }
    public int exp { get; private set; }
    public int expToNextLevel { get; private set; }

    public void LevelStartSetup()
    {
        level = startLevel;
        levelMultiplier = 1;
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
        levelMultiplier += 0.1f;
        CalculateStatIncrease();
        expToNextLevel = CalculateExpToNextLevel();
    }

    public int CalculateExpToNextLevel()
    {
        return expToNextLevel * 2;
    }

    public void CalculateStatIncrease()
    {
        health *= levelMultiplier;
        maxHealth *= levelMultiplier;
        damage *= levelMultiplier;
        dodgeSpeed *= levelMultiplier;

    }
    #endregion

    private void Awake()
    {
        PlayerStartStats();
        state = PlayerState.Normal;
    }

    //  Sets the player Start Weapon from Choice
    #region Weapon Start
    //  weaponNum will dicide what weapon is used
    [Range(1, 3)]
    public int weaponsNum;
    public GameObject[] weapons = new GameObject[3];

    private void PlayerStart(int weaponNum)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == weaponNum - 1) weapons[i].SetActive(true);
            else weapons[i].SetActive(false);
        }

        if (weaponNum == 3) anim.SetFloat("IdleBlend", 1f);
        else anim.SetFloat("IdleBlend", 0.0f);

        /*  Old Weapon Animation Setup;
        // Checks if Weapon is Sword
        #region Sword Check
        if (weaponsNum == 1) anim.SetBool("IsSword", true);
        else anim.SetBool("IsSword", false);
        #endregion

        // Checks if Weapon is Axe
        #region Axe Check
        if (weaponsNum == 2) anim.SetBool("IsAxe", true);
        else anim.SetBool("IsAxe", false);
        #endregion

        // Checks if Weapon is Bow
        #region Bow Check
        if (weaponsNum == 3)
        {
            anim.SetBool("IsBow", true);
            anim.SetFloat("IdleBlend", 0.5f);
        }
        else
        {
            anim.SetBool("IsBow", false);
            anim.SetFloat("IdleBlend", 0f);
        }
        #endregion
        */
    }
    #endregion

    private void Start()
    {

        inputManager = InputManager.Instance;

        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        GameManager.Instance.player = this;

        PlayerStart(weaponsNum);
    }

    private void Update()
    {
        moveVector = inputManager.MoveInput;
        if (moveVector != Vector2.zero) anim.SetBool("IsMoving", true);
        else anim.SetBool("IsMoving", false);

        //  Dodge Button Check
        DodgedButtonPressed = inputManager.DodgeInput;
        if (DodgedButtonPressed)
        {
            dodgeVector = moveVector;
            state = PlayerState.Dodging;
        }

        //  Attack Button Check;
        Debug.Log("canAttack is " + canAttack + " and " + AttackButtonPressed);
        AttackButtonPressed = inputManager.AttackInput;
        if (canAttack && AttackButtonPressed) state = PlayerState.Attacking;
        else state = PlayerState.Normal;

        switch (state)
        {
            case PlayerState.Normal:
                break;
            case PlayerState.Dodging:
                float dodgeSpeedMultiplier = 5f;
                dodgeSpeed -= dodgeSpeed * dodgeSpeedMultiplier * Time.deltaTime;

                float minimumDodgeSpeed = 20;
                if (dodgeSpeed < minimumDodgeSpeed)
                {
                    DodgedButtonPressed = false;
                    state = PlayerState.Normal;
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.useGravity = true;
        switch (state)
        {
            case PlayerState.Normal:
                rb.velocity = new Vector3(moveVector.x * movementSpeed, rb.velocity.y, moveVector.y * movementSpeed);

                if (moveVector != Vector2.zero)
                {
                    float targetAngle = Mathf.Atan2(moveVector.x, moveVector.y) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
                break;
            case PlayerState.Attacking:
                rb.velocity = Vector3.zero;
                if (weaponsNum == 1) PlayerSwordAttack();
                if (weaponsNum == 2) PlayerAxeAttack();
                if (weaponsNum == 3) PlayerBowAttack();
                break;

            case PlayerState.Dodging:

                Debug.Log(DodgedButtonPressed);
                if (DodgedButtonPressed)
                {
                    rb.velocity = new Vector3(dodgeVector.x * dodgeSpeed, rb.velocity.y, dodgeVector.y * dodgeSpeed);
                }
                anim.SetBool("HasDodged", DodgedButtonPressed);
                break;
        }
    }

    [Header("Player Stats - Damage")]
    public float damage;

    private float attackCooldown = 1;
    public bool canAttack = true;
    public bool AttackButtonPressed = false;

    #region Weapon Animation Functions
    public void PlayerSwordAttack()
    {
        canAttack = false;
        anim.SetTrigger("SwordAttack");
        StartCoroutine(ResetAttackCooldown());
    }
    public void PlayerAxeAttack()
    {
        canAttack = false;
        anim.SetTrigger("AxeAttack");
        StartCoroutine(ResetAttackCooldown());
    }
    public void PlayerBowAttack()
    {
        canAttack = false;
        anim.SetTrigger("BowAttack");
        weapons[2].transform.GetComponent<CrossbowManager>().OnShoot();
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        state = PlayerState.Normal;
    }
    #endregion

    //  Damage Functions
    //  - Take Damage allows the player to take damage
    //  - OnDeath is the death function that is exicuted when the players health goes below 0
    #region Damage Functions
    public void HealDamage(float healthAmount)
    {
        health += healthAmount;
        if (health > maxHealth) health = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Player has took " + damageAmount + " damage");

        if (health <= 0)
        {
            OnDeath();
        }
        else Debug.Log("Player has " + health + " health left");
    }

    // Death Function, Executed when the players health goes below 0
    public void OnDeath() 
    {
        Debug.Log("Player has died");
        GameManager.Instance.GameOver(true);
        Destroy(gameObject);
    }
    #endregion

    //------------------------------------------------------------------------------------
    //                                 Player Input Controls
    //------------------------------------------------------------------------------------
    /*
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
        anim.SetBool("IsMoving", true);
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        anim.SetBool("IsMoving", false);
    }
    #endregion

    #region Dodge Controls
    public void OnDodgePerformed(InputAction.CallbackContext context)
    {
        DodgedButtonPressed = context.ReadValueAsButton();
        dodgeVector = moveVector;
        state = PlayerState.Dodging;
    }
    public void OnDodgeCancelled(InputAction.CallbackContext context)
    {        
        DodgedButtonPressed = context.ReadValueAsButton();
    }

    #endregion

    #region Attack Controls
    public void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackButtonPressed = context.ReadValueAsButton();
        state = PlayerState.Attacking;
    }
    public void OnAttackCancelled(InputAction.CallbackContext context)
    {
        AttackButtonPressed = context.ReadValueAsButton();
    }
    #endregion
    */
}
