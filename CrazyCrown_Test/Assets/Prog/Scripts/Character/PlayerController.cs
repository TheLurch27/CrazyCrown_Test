using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSaluting = false;
    private bool isSneaking = false;


    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;

    private Vector2 moveInput;

    private void Awake()
    {
        inputSystem = new InputSystem();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Movement.Walk.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputSystem.Movement.Walk.canceled += ctx => moveInput = Vector2.zero;
        inputSystem.Salute.Salute.started += ctx => StartSalute();
        inputSystem.Salute.Salute.canceled += ctx => EndSalute();
        inputSystem.Movement.Sneaking.started += ctx => StartSneak();
        inputSystem.Movement.Sneaking.canceled += ctx => EndSneak();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Movement.Walk.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputSystem.Movement.Walk.canceled -= ctx => moveInput = Vector2.zero;
        inputSystem.Salute.Salute.started -= ctx => StartSalute();
        inputSystem.Salute.Salute.canceled -= ctx => EndSalute();
        inputSystem.Movement.Sneaking.started -= ctx => StartSneak();
        inputSystem.Movement.Sneaking.canceled -= ctx => EndSneak();
    }

    private void FixedUpdate()
    {
        if (!isSaluting)
        {
            MoveCharacter(moveInput);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        float speed = isSneaking ? sneakSpeed : moveSpeed;
        rb.velocity = direction.normalized * speed; // Richtung beibehalten und die Geschwindigkeit entsprechend setzen
        UpdateAnimation(direction);
    }

    public void StartSalute()
    {
        isSaluting = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isSaluting", true);
        animator.SetBool("isWalking", false);
    }

    private void EndSalute()
    {
        isSaluting = false;
        animator.SetBool("isSaluting", false);
    }

    public void StartSneak()
    {
        isSneaking = true;
        animator.SetBool("isSneaking", true);
    }

    private void EndSneak()
    {
        isSneaking = false;
        animator.SetBool("isSneaking", false);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public bool IsSneaking
    {
        get { return isSneaking; }
    }

    public void SetSneaking(bool sneaking)
    {
        isSneaking = sneaking;
    }
}
