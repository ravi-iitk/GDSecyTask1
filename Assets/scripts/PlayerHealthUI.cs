using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth player;      // Drag the Player object here
    public Image healthFill;         // Drag PlayerHealthBarFill here

    void Update()
    {
        float fill = (float)player.CurrentHealth / player.maxHealth;
        healthFill.fillAmount = Mathf.Clamp01(fill);
    }
}