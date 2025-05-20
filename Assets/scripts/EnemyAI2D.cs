using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    [Header("Movement & Combat")]
    public float moveSpeed = 2f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 20;
    public Transform attackPoint;
    public LayerMask playerLayer;

    [Header("Audio")]
    public AudioClip attackSound;
    private AudioSource audioSource;

    private Transform player;
    private Animator animator;
    private float lastAttackTime;
    private Rigidbody2D rb;
    private bool isFacingRight = false;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardPlayer();
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            if (Time.time - lastAttackTime > attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }

        HandleFlip();
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
    }

    void AttackPlayer()
    {
        animator.SetBool("isAttacking", true); // Use bool instead of trigger
        PlayAttackSound();
    }

    public void DealDamage() // Called by animation event
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        }
    }

    public void EndAttack() // Call this at the end of attack animation
    {
        animator.SetBool("isAttacking", false);
    }

    void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        animator.SetTrigger("Damage");
        // Reduce health here if needed
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    void HandleFlip()
    {
        if ((player.position.x > transform.position.x && !isFacingRight) ||
            (player.position.x < transform.position.x && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}