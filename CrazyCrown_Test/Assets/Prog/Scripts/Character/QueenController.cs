using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private bool isMovingRight = true;
    private SpriteRenderer spriteRenderer;
    public InGameUI lifeDisplayManager;

    public float raycastDistance = 5f;
    public LayerMask characterLayer;

    public Animator characterAnimator;

    private bool isCoolingDown = false;
    private float coolDownDuration = 5f;
    private float coolDownTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isCoolingDown)
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0f)
            {
                isCoolingDown = false;
                coolDownTimer = 0f;
            }
        }
        else
        {
            MoveNPC();

            UpdateFacingDirection();

            if (ShouldChangeDirection() && !IsCharacterSaluting())
            {
                ChangeDirection();
            }

            if (IsCharacterInSight() && !IsCharacterSaluting())
            {
                Debug.Log("Ertappt");
                StartCoolDown();
                lifeDisplayManager.OnCharacterCaught();
            }
        }
    }

    private void StartCoolDown()
    {
        isCoolingDown = true;
        coolDownTimer = coolDownDuration;
    }

    private bool ShouldChangeDirection()
    {
        // Eine Chance von 0,1% pro Frame, die Richtung zu �ndern
        return Random.value < 0.001f || HitWall();
    }

    private bool HitWall()
    {
        Vector2 direction = isMovingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, characterLayer);
        return hit.collider != null && hit.collider.CompareTag("Wall");
    }

    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }

    private void MoveNPC()
    {
        Vector2 movement = isMovingRight ? Vector2.right : Vector2.left;

        rb.velocity = movement * moveSpeed;
    }

    private void UpdateFacingDirection()
    {
        spriteRenderer.flipX = !isMovingRight;
    }

    private bool IsCharacterInSight()
    {
        Vector2 direction = isMovingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, characterLayer);

        return hit.collider != null;
    }

    private bool IsCharacterSaluting()
    {
        if (characterAnimator != null)
        {
            return characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Salute");
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
        }
        else if (other.CompareTag("Player"))
        {
            lifeDisplayManager.OnCharacterCaught();
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 direction = isMovingRight ? Vector2.right : Vector2.left;

        Vector2 raycastStart = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastStart, raycastStart + direction * raycastDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
        }
    }
}
