using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSaluting = false;

    public float moveSpeed = 5f;

    private void Awake()
    {
        inputSystem = new InputSystem();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Movement.Walk.performed += ctx => MoveCharacter(ctx.ReadValue<Vector2>());
        inputSystem.Movement.Walk.canceled += ctx => MoveCharacter(Vector2.zero);
        inputSystem.Salute.Salute.started += ctx => StartSalute();
        inputSystem.Salute.Salute.canceled += ctx => EndSalute();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Movement.Walk.performed -= ctx => MoveCharacter(ctx.ReadValue<Vector2>());
        inputSystem.Movement.Walk.canceled -= ctx => MoveCharacter(Vector2.zero);
        inputSystem.Salute.Salute.started -= ctx => StartSalute();
        inputSystem.Salute.Salute.canceled -= ctx => EndSalute();
    }

    private void FixedUpdate()
    {
        if (!isSaluting)
        {
            MoveCharacter(inputSystem.Movement.Walk.ReadValue<Vector2>());
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        if (!isSaluting)
        {
            rb.velocity = direction * moveSpeed;
            UpdateAnimation(direction);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void StartSalute()
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

    private void UpdateAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
            if (direction.x < 0) // Wenn die Richtung nach links zeigt
            {
                // Das Sprite spiegeln, wenn nach links gelaufen wird
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0) // Wenn die Richtung nach rechts zeigt
            {
                // Das Sprite wieder in die Ausgangsrichtung bringen
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
