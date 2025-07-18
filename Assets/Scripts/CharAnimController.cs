using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimController : MonoBehaviour
{
    // Movimiento
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;

    // Sonidos
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound;

    // Componentes
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    // Estado
    private float horizontalInput;
    private bool isRunning;
    private bool isGrounded;
    private bool facingRight = true;

    // Ground check
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Uso Input.GetAxisRaw para input horizontal más confiable
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isGrounded = CheckGrounded();

        foreach (var param in animator.parameters)
        {
            Debug.Log($"Animator parameter: {param.name}");
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlayOneShotSound(jumpSound);
        }

        // Actualizo animaciones
        animator.SetBool("IsJumping", !isGrounded);

        if (isGrounded)
        {
            bool isWalking = Mathf.Abs(horizontalInput) > 0.01f && !isRunning;
            bool isRunningAnim = Mathf.Abs(horizontalInput) > 0.01f && isRunning;

            animator.SetBool("IsWalking", isWalking);
            animator.SetBool("IsRunning", isRunningAnim);

            if (isRunningAnim)
                PlayLoopingSound(runSound);
            else if (isWalking)
                PlayLoopingSound(walkSound);
            else
                StopSound();
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            StopSound();
        }

        // Flip solo si hay input horizontal
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

    bool CheckGrounded()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        foreach (Collider2D hit in hits)
        {
            if (!hit.isTrigger)
                return true;
        }
        return false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void PlayOneShotSound(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.PlayOneShot(clip);
    }

    void PlayLoopingSound(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip != clip || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.loop = false;
        }
    }
}
