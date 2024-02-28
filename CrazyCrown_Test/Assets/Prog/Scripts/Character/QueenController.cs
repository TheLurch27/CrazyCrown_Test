using UnityEngine;

public class QueenController : MonoBehaviour
{

    private AudioManager audioManager;

    public Animator animator;
    public bool isAngry = false;

    private Rigidbody2D rb;
    private bool isWalking = false;
    private bool isMovingRight = true;
    private float walkSpeed = 3f;
    private float decisionTimer = 0f;
    private float decisionInterval = 1f; // Intervall für Entscheidungen in Sekunden

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isAngry", isAngry);
        DecideNextAction(); // Wähle die erste Aktion beim Start
    }

    private void Update()
    {
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (isMovingRight)
            {
                rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Stoppe die Bewegung, wenn nicht gegangen wird
        }

        decisionTimer += Time.deltaTime;
        if (decisionTimer >= decisionInterval)
        {
            DecideNextAction();
            decisionTimer = 0f;
        }
    }

    private void DecideNextAction()
    {
        // Zufällig entscheiden, ob der NPC geht oder nicht
        isWalking = Random.value > 0.5f; // 50% Wahrscheinlichkeit zu gehen
        if (isWalking)
        {
            // Zufällig entscheiden, in welche Richtung der NPC geht
            isMovingRight = Random.value > 0.5f; // 50% Wahrscheinlichkeit nach rechts zu gehen
            transform.localScale = new Vector3(isMovingRight ? 1 : -1, 1, 1);
        }
    }
}
