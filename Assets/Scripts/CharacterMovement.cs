using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3f;
    public AudioSource walkSound;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0) movement.y = 0;

        UpdateAnimation();

        if (movement != Vector2.zero)
        {
            if (!walkSound.isPlaying)
                walkSound.Play();
        }
        else
        {
            if (walkSound.isPlaying)
                walkSound.Stop();
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void UpdateAnimation()
    {
        float speedValue = movement.magnitude;
        animator.SetFloat("Speed", speedValue);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
    }
}
