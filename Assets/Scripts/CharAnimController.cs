using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimController : MonoBehaviour
{
    //Movimiento
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;

    //Sonidos
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound;

    //Componentes
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    //Estado del personaje
    private float horizontalInput;
    private bool isRunning;
    private bool isGrounded;
    private bool isJumping;
    private bool facingRight = true;

    //Ground check
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

   void Update()
    {
        // Entrada horizontal con A y D
        horizontalInput = 0f;
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Salto con W
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("IsJumping", true);
            PlaySound(jumpSound);
        }

        // Aterrizaje: cuando estaba en el aire y ahora está en el suelo
        if (!wasGrounded && isGrounded)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        // Animaciones solo si NO está saltando
        if (!isJumping)
        {
            if (Mathf.Abs(horizontalInput) > 0)
            {
                if (isRunning)
                {
                    animator.Play("CharRun");
                    PlaySoundOnce(runSound);
                }
                else
                {
                    animator.Play("CharWalk");
                    PlaySoundOnce(walkSound);
                }
            }
            else
            {
                animator.Play("CharIddle");
                StopSound();
            }
        }

        // Flip del sprite
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    void PlaySoundOnce(AudioClip clip)
    {
        if (clip == null) return;

        if (!audioSource.isPlaying || audioSource.clip != clip)
        {
            PlaySound(clip);
        }
    }

    void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
