using UnityEngine;

public class ButlerController : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float stoppingDistance = 2f;

    private Transform playerTransform;
    private bool isMoving = false;

    private void Start()
    {
        // Den Spieler finden
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;

            // Bewegung starten
            StartMoving();
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void Update()
    {
        if (isMoving && playerTransform != null)
        {
            // Berechne die Richtung zum Spieler
            Vector3 direction = playerTransform.position - transform.position;
            direction.z = 0f; // Stellen Sie sicher, dass die Bewegung nur in der Ebene bleibt

            // Überprüfe, ob der Butler die gewünschte Entfernung zum Spieler erreicht hat
            if (direction.magnitude > stoppingDistance)
            {
                // Bewege den Butler in Richtung des Spielers
                transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
            }
            else
            {
                // Stoppe die Bewegung, wenn die gewünschte Entfernung erreicht ist
                StopMoving();
            }
        }
    }

    private void StartMoving()
    {
        isMoving = true;
    }

    private void StopMoving()
    {
        isMoving = false;
    }
}
