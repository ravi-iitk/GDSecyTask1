using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    [Header("Optional")]
    public MonoBehaviour enemyAI; // Assign your AI/movement script in Inspector

    private EnemyHealthUI healthUI; // Reference to the health UI script

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (enemyAI == null)
            enemyAI = GetComponent<MonoBehaviour>();

        // Find and assign the correct health UI
        healthUI = FindObjectOfType<EnemyHealthUI>();

        if (healthUI != null)
        {
            healthUI.enemy = this.transform;
            healthUI.healthBarContainer.SetActive(false); // Start hidden
        }

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger("Damage");

        UpdateHealthUI();

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

        if (enemyAI != null)
            enemyAI.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        FindObjectOfType<GameUIManager>()?.ShowLevelComplete();

        if (healthUI != null)
            healthUI.healthBarContainer.SetActive(false);

        Destroy(gameObject, 2f);
    }

    void UpdateHealthUI()
    {
        if (healthUI != null)
        {
            healthUI.UpdateHealthBar(currentHealth, maxHealth);

            // Show the health bar if player is nearby
            float distanceToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if (distanceToPlayer < 5f && !isDead)
            {
                healthUI.healthBarContainer.SetActive(true);
            }
            else
            {
                healthUI.healthBarContainer.SetActive(false);
            }
        }
    }
}