using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [Header("Target References")]
    public Transform enemy;
    public Transform player;

    [Header("UI References")]
    public GameObject healthBarContainer;
    public Image healthFill;
    public Text healthLabel;

    [Header("Settings")]
    public float visibilityDistance = 8f;
    public Vector3 offset = new Vector3(0, 2f, 0); // Offset above enemy

    void Update()
    {
        if (enemy == null || player == null || healthBarContainer == null) return;

        float distance = Vector2.Distance(player.position, enemy.position);
        bool isVisible = distance < visibilityDistance;

        healthBarContainer.SetActive(isVisible);

        if (isVisible)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.position + offset);
            transform.position = screenPos;
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)currentHealth / maxHealth;

        if (healthLabel != null)
            healthLabel.text = "Enemy Health";
    }
}