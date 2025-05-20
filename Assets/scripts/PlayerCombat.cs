using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private bool canDoAttack2 = false;

    [Header("Combat Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 20;
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public float attackCooldown = 0.5f;

    [Header("Audio")]
    public AudioClip attackSound;
    private AudioSource audioSource;

    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("ReadyToAttack");
                PlayAttackSound();
                DealDamage();
            }
            else if (canDoAttack2)
            {
                animator.SetTrigger("DoAttack2");
                canDoAttack2 = false;
                PlayAttackSound();
                DealDamage();
            }
        }
    }

    void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    public void AllowAttack2() => canDoAttack2 = true;

    public void ResetAttackState()
    {
        isAttacking = false;
        canDoAttack2 = false;
        canAttack = true;
    }

    void DealDamage()
    {
        if (!canAttack) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }

        canAttack = false;
        Invoke(nameof(ResetCanAttack), attackCooldown);
    }

    void ResetCanAttack() => canAttack = true;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Damage");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        PlayerMovement move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}