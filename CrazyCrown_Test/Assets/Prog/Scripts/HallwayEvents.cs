using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayEvents : MonoBehaviour
{

    [Header("•••••••Salute Event•••••••")]
    [Header("")]
    public GameObject saluteEvent;
    public Animator animator;
    public AudioSource audioSource;

    private bool saluteTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Salute");
            saluteTriggered = true;
        }
    }

    private void Update()
    {
        if (saluteTriggered && animator.GetCurrentAnimatorStateInfo(0).IsName("SaluteAnimation"))
        {
            audioSource.Play();
            saluteTriggered = false;
        }
    }
}
