using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    public int CurrentHealth => currentHealth;

    // Y position below which the player dies instantly
    public float fallDeathY = -17f;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckFallDeath();
    }

    void CheckFallDeath()
    {
        if (transform.position.y < fallDeathY && !isDead)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
{
    if (isDead) return;
    isDead = true;

    animator.SetBool("isDead", true);

    PlayerMovement movement = GetComponent<PlayerMovement>();
    if (movement != null)
        movement.enabled = false;

    rb.linearVelocity = Vector2.zero;
    rb.isKinematic = true;

    // Show Game Over UI
    FindObjectOfType<GameUIManager>()?.ShowGameOver();
}
}